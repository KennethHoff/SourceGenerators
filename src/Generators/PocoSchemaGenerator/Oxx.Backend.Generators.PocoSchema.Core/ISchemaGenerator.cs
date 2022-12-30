namespace Oxx.Backend.Generators.PocoSchema.Core;

public interface ISchemaGenerator
{
	/// <summary> Generates all schema files - whatever that entails </summary>
	Task GenerateAllAsync();

	/// <summary> Generates a schema file for the given type </summary>
	/// <param name="includeDependencies"> If true, all dependencies of the given type will be included in the schema file </param>
	/// <typeparam name="TPoco"> The type to generate a schema file for </typeparam>
	Task GenerateAsync<TPoco>(bool includeDependencies = true);

	/// <summary> Generates a schema file for the given type </summary>
	/// <param name="pocoType"> The type to generate a schema file for </param>
	/// <param name="includeDependencies"> If true, all dependencies of the given type will be included in the schema file </param>
	Task GenerateAsync(Type pocoType, bool includeDependencies = true);

	/// <summary> Generates schema files for the given types </summary> 
	/// <param name="pocoTypes"> The types to generate a schema file for </param>
	/// <param name="includeDependencies"> If true, all dependencies of the given types will be included in the schema file </param>
	Task GenerateAsync(IEnumerable<Type> pocoTypes, bool includeDependencies = true);
}