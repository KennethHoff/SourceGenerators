using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public class SchemaGenerator
{
	private readonly ISchema _schema;
	private readonly ISchemaGeneratorConfiguration _configuration;

	public SchemaGenerator(ISchema schema, ISchemaGeneratorConfiguration configuration)
	{
		_schema = schema;
		_configuration = configuration;
	}

	public bool CreateFiles()
	{
		EnsureDirectoryExists();
		var pocoObjects = GetPocoObjects();
		var contents = _schema.GenerateFileContent(pocoObjects);
		
		foreach (var (fileName, fileContent) in contents)
		{
			File.WriteAllText(fileName, fileContent);
		}

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