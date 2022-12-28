using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Models;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IPartialZodSchema, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	protected override ZodSchemaConfiguration Configuration => new()
	{
		SchemaToCreateDictionary = AppliedSchemaTypeDictionary,
		GenericSchemaDictionary = GenericSchemaTypeDictionary,
		Assemblies = Assemblies,
		OutputDirectory = OutputDirectory,
		DeleteFilesOnStart = DeleteFilesOnStart,
		Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
		SchemaNamingFormat = SchemaNamingFormat,
		SchemaTypeNamingFormat = SchemaTypeNamingFormat,
		SchemaFileNameFormat = FileNameFormat,
		FileExtension = FileExtension,
		CreatedSchemaDictionary = new TypeSchemaDictionary<IPartialZodSchema>(),
	};

	protected override string FileExtension { get; set; } = ".ts";
	protected override string FileNameFormat { get; set; } = "{0}Schema";
	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";

	public ZodSchemaConfigurationBuilder()
	{
		ApplySchemas(() =>
		{
			ApplySchema<string, StringBuiltInAtomicZodSchema>();
			ApplySchema<int, NumberBuiltInAtomicZodSchema>();
			ApplySchema<float, NumberBuiltInAtomicZodSchema>();
			ApplySchema<double, NumberBuiltInAtomicZodSchema>();
			ApplySchema<decimal, NumberBuiltInAtomicZodSchema>();
			ApplySchema<Guid, GuidAtomicZodSchema>();
			ApplySchema<bool, BooleanBuiltInAtomicZodSchema>();
			ApplySchema<DateTime, DateBuiltInAtomicZodSchema>();
			ApplyGenericSchema(typeof(IEnumerable<>), typeof(ArrayBuiltInAtomicZodSchema<>));
		});
	}
}
