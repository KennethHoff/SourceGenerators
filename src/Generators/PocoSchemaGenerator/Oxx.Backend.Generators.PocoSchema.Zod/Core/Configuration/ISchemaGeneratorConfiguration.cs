using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Core.Configuration;

public interface ISchemaGeneratorConfiguration<TSchemaType> : ISchemaGeneratorConfiguration
	where TSchemaType: ISchemaType
{ }

public interface ISchemaGeneratorConfiguration
{
	IList<Assembly> Assemblies { get; }
	string OutputDirectory { get; }
	bool DeleteFilesOnStart { get; }
	string SchemaNamingConvention { get; }
	string SchemaTypeNamingConvention { get; }
}

public class ZodSchemaGeneratorConfiguration : ISchemaGeneratorConfiguration<IZodSchemaType>
{
	public IList<Assembly> Assemblies { get; set; } = new List<Assembly>();
	public string OutputDirectory { get; set; } = string.Empty;
	public bool DeleteFilesOnStart { get; set; } = true;
	public IDictionary<Type, IZodSchemaType> SchemaTypeDictionary { get; set; } = new Dictionary<Type, IZodSchemaType>();
	public string SchemaNamingConvention { get; set; } = "{0}Schema";
	public string SchemaTypeNamingConvention { get; set; } = "{0}SchemaType";
}