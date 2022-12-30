﻿using System.Reflection;
using System.Runtime.CompilerServices;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Extensions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;

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
		var (pocoStructures, unsupportedTypes) = GetPocoStructures();
		_configuration.Events.PocoStructuresCreated?.Invoke(this, new PocoStructuresCreatedEventArgs(pocoStructures, unsupportedTypes));
		var contents = _schemaConverter.GenerateFileContent(pocoStructures).ToList();

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

	private (IPocoStructure[] pocoStructures, List<(Type Type, Exception Exception)> unsupportedTypes) GetPocoStructures()
	{
		var (types, unsupportedTypes) = GetTypeSchemaDictionary();

		var objectTypes = types
			.FirstOrDefault(x => x.Key is SchemaObjectAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var enumTypes = types
			.FirstOrDefault(x => x.Key is SchemaEnumAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var objects = objectTypes
			.Select(t =>
			{
				var validSchemaMembers = t.GetValidSchemaMembers();
				return new PocoObject(t, validSchemaMembers);
			})
			.Cast<IPocoStructure>()
			.ToArray();

		var enums = enumTypes
			.Select(t => new PocoEnum(t))
			.Cast<IPocoStructure>()
			.ToArray();

		var pocoStructures = objects.Concat(enums).ToArray();
		return (pocoStructures, unsupportedTypes);
	}

	private (Dictionary<SchemaTypeAttribute, List<Type>> types, List<(Type Type, Exception Exception)> unsupportedTypes) GetTypeSchemaDictionary()
	{
		var types = new Dictionary<SchemaTypeAttribute, List<Type>>();
		var unsupportedTypes = new List<(Type Type, Exception Exception)>();
		foreach (var assembly in _configuration.Assemblies)
		{
			foreach (var type in assembly.GetTypes())
			{
				var schemaTypeAttribute = type.GetCustomAttribute<SchemaTypeAttribute>();
				if (schemaTypeAttribute is null)
				{
					continue;
				}

				if (IsSupported(type) is {} exception)
				{
					unsupportedTypes.Add((type, exception));
					// _configuration.Events.UnsupportedTypeFound?.Invoke(this, new UnsupportedTypeFoundEventArgs(type, exception));
					continue;
				}

				if (!types.ContainsKey(schemaTypeAttribute))
				{
					types.Add(schemaTypeAttribute, new List<Type>());
				}

				types[schemaTypeAttribute].Add(type);
			}
		}
		
		return (types, unsupportedTypes);
	}

	private static Exception? IsSupported(Type type)
	{
		if (type.IsGenericType)
		{
			return new ArgumentException("Generic types are not supported");
		}

		var validMemberInfos = type.GetValidSchemaMembers().ToArray();
		if (!validMemberInfos.Any())
		{
			return new ArgumentException("No fields or properties found");
		}

		return null;
	}
}
