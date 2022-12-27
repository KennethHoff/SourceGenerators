using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IPartialZodSchema, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	public ZodSchemaConfigurationBuilder()
	{
		ApplySchemas(() =>
		{
			ApplySchema<string, StringAtomicZodSchema>();
			ApplySchema<int, NumberAtomicZodSchema>();
			ApplySchema<float, NumberAtomicZodSchema>();
			ApplySchema<double, NumberAtomicZodSchema>();
			ApplySchema<decimal, NumberAtomicZodSchema>();
			ApplySchema<Guid, GuidAtomicZodSchema>();
			ApplySchema<bool, BooleanAtomicZodSchema>();
			ApplySchema<DateTime, DateAtomicZodSchema>();
			ApplyGenericSchema(typeof(IEnumerable<>), typeof(ArrayAtomicZodSchema<>));
		});
	}
	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";
	protected override string FileNameFormat { get; set; } = "{0}Schema";
	protected override ZodSchemaConfiguration Configuration => new()
	{
		SchemaDictionary = SchemaTypeDictionary,
		GenericSchemaDictionary = GenericSchemaTypeDictionary,
		Assemblies = Assemblies,
		OutputDirectory = OutputDirectory,
		DeleteFilesOnStart = DeleteFilesOnStart,
		Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
		SchemaNamingFormat = SchemaNamingFormat,
		SchemaTypeNamingFormat = SchemaTypeNamingFormat,
		SchemaFileNameFormat = FileNameFormat,
		FileExtension = FileExtension,
	};
	protected override string FileExtension { get; set; } = ".ts";
}