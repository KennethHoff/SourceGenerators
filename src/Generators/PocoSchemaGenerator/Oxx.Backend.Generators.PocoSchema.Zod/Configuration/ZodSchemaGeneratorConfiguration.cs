using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaGeneratorConfiguration : ISchemaGeneratorConfiguration<IZodSchemaType>
{
	public IEnumerable<Assembly> Assemblies { get; set; } = new List<Assembly>();
	public string OutputDirectory { get; set; } = string.Empty;
	public bool DeleteFilesOnStart { get; set; } = true;
	public IDictionary<Type, IZodSchemaType> SchemaTypeDictionary { get; set; } = new Dictionary<Type, IZodSchemaType>();
	public string SchemaNamingConvention { get; set; } = "{0}Schema";
	public string SchemaTypeNamingConvention { get; set; } = "{0}SchemaType";
}