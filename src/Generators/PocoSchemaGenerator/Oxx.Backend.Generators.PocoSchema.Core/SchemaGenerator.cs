﻿using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Poco.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schema.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaGenerator<TSchemaType, TSchemaEventConfiguration>
	where TSchemaType : ISchema
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	private readonly ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> _configuration;
	private readonly ISchemaConverter _schemaConverter;

	protected SchemaGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> configuration)
	{
		_schemaConverter = schemaConverter;
		_configuration = configuration;
	}

	public async Task CreateFilesAsync()
	{
		EnsureDirectoryExists();
		var pocoStructures = GetPocoStructures();
		var contents = _schemaConverter.GenerateFileContent(pocoStructures).ToList();

		_configuration.Events.FilesCreating?.Invoke(this, new FilesCreatingEventArgs(contents));

		await Task.WhenAll(contents.Select(fileInformation => Task.Run(async () =>
		{
			var fileCreatingEventArgs = new FileCreatingEventArgs(fileInformation);
			_configuration.Events.FileCreating?.Invoke(this, fileCreatingEventArgs);

			if (!fileCreatingEventArgs.Skip)
			{
				var filePath = Path.Combine(_configuration.OutputDirectory, fileInformation.Name + _configuration.FileExtension);

				await File.WriteAllTextAsync(filePath, fileInformation.Content);
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

	private IReadOnlyCollection<IPocoStructure> GetPocoStructures()
	{
		var types = new Dictionary<SchemaTypeAttribute, List<Type>>();
		foreach (var assembly in _configuration.Assemblies)
		{
			foreach (var type in assembly.GetTypes())
			{
				var schemaTypeAttribute = type.GetCustomAttribute<SchemaTypeAttribute>();
				if (schemaTypeAttribute is null)
				{
					continue;
				}

				if (!types.ContainsKey(schemaTypeAttribute))
				{
					types.Add(schemaTypeAttribute, new List<Type>());
				}

				types[schemaTypeAttribute].Add(type);
			}
		}

		var objects = types.FirstOrDefault(x => x.Key is SchemaObjectAttribute)
			.Value
			.Select(t =>
			{
				var relevantProperties = GetRelevantProperties(t);
				return new PocoObject(t, relevantProperties);
			})
			.Cast<IPocoStructure>()
			.ToArray();

		var enums = types.FirstOrDefault(x => x.Key is SchemaEnumAttribute)
			.Value
			.Select(t =>
			{
				var enumType = t.GetCustomAttribute<SchemaEnumAttribute>()?.EnumType;
				if (enumType is null)
				{
					throw new InvalidOperationException($"Enum type is not defined for {t.Name}");
				}

				return new PocoEnum(enumType);
			})
			.Cast<IPocoStructure>()
			.ToArray();

		return objects.Concat(enums).ToArray();
	}

	private static Func<PropertyInfo, bool> DoesNotHaveIgnoreAttribute()
		=> pi => pi.GetCustomAttribute<SchemaPropertyIgnoreAttribute>() is null;

	private static IEnumerable<PropertyInfo> GetRelevantProperties(Type type)
		=> type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			.Where(IsPropertyOrField())
			.Where(DoesNotHaveIgnoreAttribute());

	// More efficient than HasFlag.. I think - Rider gave me allocation warnings with HasFlag
	private static Func<PropertyInfo, bool> IsPropertyOrField()
		=> pi => (pi.MemberType & MemberTypes.Property) != 0 || (pi.MemberType & MemberTypes.Field) != 0;
}
