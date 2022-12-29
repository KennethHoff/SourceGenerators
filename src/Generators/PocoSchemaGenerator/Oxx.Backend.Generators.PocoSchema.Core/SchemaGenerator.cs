using System.Reflection;
using System.Runtime.CompilerServices;
using Oxx.Backend.Generators.PocoSchema.Core.Attributes;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Pocos.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Schemas.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;

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

	private IReadOnlyCollection<IPocoStructure> GetPocoStructures()
	{
		var typeSchemaDictionary = GetTypeSchemaDictionary();

		var objectTypes = typeSchemaDictionary
			.FirstOrDefault(x => x.Key is SchemaObjectAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var enumTypes = typeSchemaDictionary
			.FirstOrDefault(x => x.Key is SchemaEnumAttribute, new KeyValuePair<SchemaTypeAttribute, List<Type>>(default!, new List<Type>()))
			.Value;

		var objects = objectTypes
			.Select(t =>
			{
				var propertiesAndFields = GetRelevantPropertiesAndFields(t);
				return new PocoObject(t, propertiesAndFields);
			})
			.Cast<IPocoStructure>()
			.ToArray();

		var enums = enumTypes
			.Select(t => new PocoEnum(t))
			.Cast<IPocoStructure>()
			.ToArray();

		return objects.Concat(enums).ToArray();
	}

	private Dictionary<SchemaTypeAttribute, List<Type>> GetTypeSchemaDictionary()
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

				// Throw exception if Type is Generic
				if (type.IsGenericType)
				{
					throw new InvalidOperationException($"Type {type.FullName} is generic and cannot be used as a schema type.");
				}

				if (!types.ContainsKey(schemaTypeAttribute))
				{
					types.Add(schemaTypeAttribute, new List<Type>());
				}

				types[schemaTypeAttribute].Add(type);
			}
		}

		return types;
	}

	private static IEnumerable<SchemaMemberInfo> GetRelevantPropertiesAndFields(Type type)
	{
		const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
		IEnumerable<MemberInfo> properties = type.GetProperties(bindingFlags);
		IEnumerable<MemberInfo> fields = type.GetFields(bindingFlags);

		return properties.Concat(fields)
			.Where(x => x.GetCustomAttribute<CompilerGeneratedAttribute>() is null)
			.Select(x => new SchemaMemberInfo(x))
			.Where(x => x.IsIgnored is false);
	}
}
