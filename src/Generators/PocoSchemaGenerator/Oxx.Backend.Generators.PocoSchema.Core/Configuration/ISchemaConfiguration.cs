using System.Reflection;

namespace Oxx.Backend.Generators.PocoSchema.Core.Configuration;

public interface ISchemaConfiguration<TSchemaType> : ISchemaConfiguration
	where TSchemaType: ISchemaType
{ }

public interface ISchemaConfiguration
{
	IEnumerable<Assembly> Assemblies { get; }
	string OutputDirectory { get; }
	bool DeleteFilesOnStart { get; }
	string SchemaNamingConvention { get; }
	string SchemaTypeNamingConvention { get; }
}
