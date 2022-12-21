using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Events;
using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public abstract class SchemaGenerator<TSchemaType, TSchemaEventConfiguration>
	where TSchemaType : ISchemaType
	where TSchemaEventConfiguration : ISchemaEventConfiguration
{
	private readonly ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> _configuration;
	private readonly ISchema _schema;

	protected SchemaGenerator(ISchema schema, ISchemaConfiguration<TSchemaType, TSchemaEventConfiguration> configuration)
	{
		_schema = schema;
		_configuration = configuration;
	}

	public bool CreateFiles()
	{
		EnsureDirectoryExists();
		var pocoObjects = GetPocoObjects();
		var contents = _schema.GenerateFileContent(pocoObjects).ToList();
		
		_configuration.Events.FilesCreating?.Invoke(this, new FilesCreatingEventArgs(contents));
		foreach (var fileInformation in contents)
		{
			var fileCreatingEventArgs = new FileCreatingEventArgs(fileInformation);
			_configuration.Events.FileCreating?.Invoke(this, fileCreatingEventArgs);
			if (!fileCreatingEventArgs.Skip)
			{
				var filePath = Path.Combine(_configuration.OutputDirectory, fileInformation.Name);
				File.WriteAllText(filePath, fileInformation.Content);
			}
			_configuration.Events.FileCreated?.Invoke(this, new FileCreatedEventArgs(fileInformation)
			{
				Skipped = fileCreatingEventArgs.Skip,
			});
		}
		_configuration.Events.FilesCreated?.Invoke(this, new FilesCreatedEventArgs(contents));

		return true;
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
			return new PocoObject(new BaseName(t.Name), relevantProperties);
		});
	}

	private static IEnumerable<PropertyInfo> GetRelevantProperties(Type type)
		=> type.GetProperties()
			.Where(IsPropertyOrField());

	// More efficient than HasFlag.. I think - Rider gave me allocation warnings with HasFlag
	private static Func<PropertyInfo, bool> IsPropertyOrField()
		=> pi => (pi.MemberType & MemberTypes.Property) != 0 || (pi.MemberType & MemberTypes.Field) != 0;
}