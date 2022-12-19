using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaGeneratorConfiguration<TSchemaType> : ISchemaGeneratorConfiguration
	where TSchemaType: ISchemaType
{ }

public interface ISchemaGeneratorConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	string OutputDirectory { get; }
	bool DeleteFilesOnStart { get; }
	string SchemaNamingConvention { get; }
	string SchemaTypeNamingConvention { get; }
}
