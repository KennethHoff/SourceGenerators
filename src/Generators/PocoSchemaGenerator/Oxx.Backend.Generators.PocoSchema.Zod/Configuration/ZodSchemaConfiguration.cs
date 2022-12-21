using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<IAtomicZodSchema, ZodSchemaEventConfiguration>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required string OutputDirectory { get; init; }
	public required bool DeleteFilesOnStart { get; init; } = true;
	public required IDictionary<Type, IAtomicZodSchema> AtomicSchemaDictionary { get; init; }
	public required string SchemaNamingFormat { get; init; } = "{0}Schema";
	public required string SchemaTypeNamingFormat { get; init; } = "{0}SchemaType";
	public required string SchemaFileNameFormat { get; init; } = "{0}Schema.ts";
	public required ZodSchemaEventConfiguration Events { get; init; }

	public string FormatSchemaTypeName(IZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaTypeNamingFormat, schema.SchemaBaseName);

	public string FormatSchemaName(IZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaNamingFormat, schema.SchemaBaseName);

	public string FormatFilePath(IZodSchema zodSchema)
		=> $"./{string.Format(SchemaFileNameFormat, zodSchema.SchemaBaseName)}".TrimEnd('.', 't', 's');
}