using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core;

public sealed class SchemaGenerator
{
	private readonly ISchema _schema;
	private readonly IList<Assembly> _assemblies;
	private readonly string _outputDirectory;

	public SchemaGenerator(ISchema schema, SchemaGeneratorConfigurationBuilder configurationBuilder)
	{
		_schema = schema;
		_assemblies = configurationBuilder.Assemblies;
		_outputDirectory = configurationBuilder.OutputDirectory;
	}

	public IReadOnlyCollection<FileInformation> CreateFiles()
	{
		var pocoObjects = GetPocoObjects();
		var contents = _schema.GenerateFileContent(pocoObjects).ToArray();
		
		foreach (var (fileName, fileContent) in contents)
		{
			var filePath = Path.Combine(_outputDirectory, fileName);
			File.WriteAllText(filePath, fileContent);
		}

		return contents;
	}
	
	private IEnumerable<PocoObject> GetPocoObjects()
	{
		var types = new List<Type>();
		foreach (var assembly in _assemblies)
		{
			types.AddRange(assembly.GetTypes().Where(t => t.GetCustomAttribute<PocoObjectAttribute>() is not null).ToList());
		}
		
		return types.Select(t =>
		{
			var relevantProperties = GetRelevantProperties(t);
			return new PocoObject(t.FullName!, relevantProperties);
		});
	}
	
	private static IEnumerable<PropertyInfo> GetRelevantProperties(Type type)
		=> type.GetProperties()
			.Where(IsPropertyOrField());

	// More efficient than HasFlag.. I think - Rider gave me allocation warnings with HasFlag
	private static Func<PropertyInfo, bool> IsPropertyOrField()
		=> pi => (pi.MemberType & MemberTypes.Property) != 0 || (pi.MemberType & MemberTypes.Field) != 0;
}