using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Core.Models.Types;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IPartialZodSchema, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	protected override ZodSchemaConfiguration Configuration => new()
	{
		AtomicSchemasToCreateDictionary = AtomicSchemasToCreateDictionary,
		GenericSchemasDictionary = GenericSchemasDictionary,
		Assemblies = Assemblies,
		OutputDirectory = OutputDirectory,
		FileDeletionMode = FileDeletionMode,
		Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
		SchemaNamingFormat = SchemaNamingFormat,
		SchemaEnumNamingFormat = SchemaEnumNamingFormat,
		SchemaTypeNamingFormat = SchemaTypeNamingFormat,
		SchemaFileNameFormat = FileNameFormat,
		FileExtension = FileExtension,
		FileExtensionInfix = FileExtensionInfix,
		CreatedSchemasDictionary = new TypeSchemaDictionary<IPartialZodSchema>(),
	};

	protected override string FileExtension { get; set; } = ".ts";
	protected override string FileExtensionInfix { get; set; } = string.Empty;
	protected override string FileNameFormat { get; set; } = "{0}Schema";
	protected override string SchemaEnumNamingFormat { get; set; } = "{0}SchemaEnum";
	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";

	public ZodSchemaConfigurationBuilder()
	{
		ApplyAtomicSchemas(() =>
		{
			ApplyAtomicSchema<string, StringBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<int, NumberBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<float, NumberBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<double, NumberBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<decimal, NumberBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<Guid, GuidAtomicZodSchema>();
			ApplyAtomicSchema<bool, BooleanBuiltInAtomicZodSchema>();
			ApplyAtomicSchema<DateTime, DateBuiltInAtomicZodSchema>();
			ApplyGenericSchema(typeof(IEnumerable<>), typeof(ArrayBuiltInAtomicZodSchema<>));
		});
	}
}
