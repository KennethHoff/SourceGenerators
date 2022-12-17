using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core;

public sealed class SchemaGenerator
{
	private readonly ISchema _schema;
	private readonly string _outputDirectory;
	private readonly IList<Assembly> _assemblies;


	public SchemaGenerator(ISchema schema, SchemaGeneratorConfigurationBuilder configurationBuilder)
	{
		_schema = schema;
		_outputDirectory = configurationBuilder.OutputDirectory;
		_assemblies = configurationBuilder.Assemblies;
	}

	public bool CreateFiles()
	{
		var pocoObjects = GetPocoObjects();
		return _schema.Generate(pocoObjects);
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