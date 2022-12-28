﻿using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaGenerator<TSchemaType, TSchemaEventConfiguration>
	where TSchemaType : ISchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	private readonly ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> _configuration;
	private readonly ISchemaConverter _schemaConverter;
	private readonly SemaphoreSlim _semaphoreSlim = new(1);

	protected SchemaGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> configuration)
	{
		_schemaConverter = schemaConverter;
		_configuration = configuration;
	}

	public async Task CreateFilesAsync()
	{
		EnsureDirectoryExists();
		var pocoObjects = GetPocoObjects();
		var contents = _schemaConverter.GenerateFileContent(pocoObjects).ToList();
		
		_configuration.Events.FilesCreating?.Invoke(this, new FilesCreatingEventArgs(contents));

		await Task.WhenAll(contents.Select(fileInformation => Task.Run(async () =>
			{
				var fileCreatingEventArgs = new FileCreatingEventArgs(fileInformation);
				_configuration.Events.FileCreating?.Invoke(this, fileCreatingEventArgs);

				if (!fileCreatingEventArgs.Skip)
				{
					var filePath = Path.Combine(_configuration.OutputDirectory, fileInformation.Name + _configuration.FileExtension);
					
					await _semaphoreSlim.WaitAsync();
					await File.WriteAllTextAsync(filePath, fileInformation.Content);
					_semaphoreSlim.Release();
				}

				_configuration.Events.FileCreated?.Invoke(this, new FileCreatedEventArgs(fileInformation)
				{
					Skipped = fileCreatingEventArgs.Skip,
				});
			})));

		_configuration.Events.FilesCreated?.Invoke(this, new FilesCreatedEventArgs(contents));
	}

	private void EnsureDirectoryExists()
	{
		if (_configuration.DeleteFilesOnStart && Directory.Exists(_configuration.OutputDirectory))
		{
			Directory.Delete(_configuration.OutputDirectory, true);
		}
		Directory.CreateDirectory(_configuration.OutputDirectory);
	}

	private IEnumerable<PocoObject> GetPocoObjects()
	{
		var types = new List<Type>();
		foreach (var assembly in _configuration.Assemblies)
		{
			types.AddRange(assembly.GetTypes().Where(t => t.GetCustomAttribute<PocoObjectAttribute>() is not null).ToList());
		}

		return types.Select(t =>
		{
			var relevantProperties = GetRelevantProperties(t);
			return new PocoObject(t, relevantProperties);
		});
	}

	private static IEnumerable<PropertyInfo> GetRelevantProperties(Type type)
		=> type.GetProperties()
			.Where(IsPropertyOrField())
			.Where(DoesNotHaveIgnoreAttribute());

	// More efficient than HasFlag.. I think - Rider gave me allocation warnings with HasFlag
	private static Func<PropertyInfo, bool> IsPropertyOrField()
		=> pi => (pi.MemberType & MemberTypes.Property) != 0 || (pi.MemberType & MemberTypes.Field) != 0;
	
	private static Func<PropertyInfo, bool> DoesNotHaveIgnoreAttribute()
		=> pi => pi.GetCustomAttribute<PocoPropertyIgnoreAttribute>() is null;
}