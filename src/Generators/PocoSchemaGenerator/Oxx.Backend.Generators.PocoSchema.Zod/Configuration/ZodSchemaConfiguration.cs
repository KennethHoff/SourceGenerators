using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfiguration : ISchemaConfiguration<IAtomicZodSchema, ZodSchemaEventConfiguration>
{
	public required IEnumerable<Assembly> Assemblies { get; init; }
	public required string OutputDirectory { get; init; }
	public required bool DeleteFilesOnStart { get; init; }
	public required IDictionary<Type, IAtomicZodSchema> AtomicSchemaDictionary { get; init; }
	public required string SchemaNamingFormat { get; init; }
	public required string SchemaTypeNamingFormat { get; init; }
	public required string SchemaFileNameFormat { get; init; }
	public required ZodSchemaEventConfiguration Events { get; init; }
	public required string FileExtension { get; init; }

	public string FormatSchemaTypeName(IPartialZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaTypeNamingFormat, schema.SchemaBaseName);

	public string FormatSchemaName(IPartialZodSchema schema)
		=> schema is IBuiltInAtomicZodSchema
			? schema.SchemaBaseName
			: string.Format(SchemaNamingFormat, schema.SchemaBaseName);

	// If we ever add a way to configure the output file directory, this will need to be updated.
	// Currently it assumes everything is in the same directory.
	public string FormatFilePath(IPartialZodSchema zodSchema)
		=> $"./{string.Format(SchemaFileNameFormat, zodSchema.SchemaBaseName)}";
}