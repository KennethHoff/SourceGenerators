using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IAtomicZodSchema, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	public ZodSchemaConfigurationBuilder()
	{
		ApplySchemaToClass<string, StringAtomicZodSchema>();
		ApplySchemaToStruct<int, NumberAtomicZodSchema>();
		ApplySchemaToStruct<float, NumberAtomicZodSchema>();
		ApplySchemaToStruct<double, NumberAtomicZodSchema>();
		ApplySchemaToStruct<decimal, NumberAtomicZodSchema>();
		ApplySchemaToStruct<Guid, GuidAtomicZodSchema>();
		ApplySchemaToStruct<bool, BooleanAtomicZodSchema>();
		ApplySchemaToStruct<DateTime, DateAtomicZodSchema>();
	}

	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";
	protected override string FileNameFormat { get; set; } = "{0}Schema";
	protected override string FileExtension { get; set; } = ".ts";

	protected override ZodSchemaConfiguration CreateConfiguration()
		=> new()
		{
			AtomicSchemaDictionary = SchemaTypeDictionary,
			Assemblies = Assemblies,
			OutputDirectory = OutputDirectory,
			DeleteFilesOnStart = DeleteFilesOnStart,
			Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
			SchemaNamingFormat = SchemaNamingFormat,
			SchemaTypeNamingFormat = SchemaTypeNamingFormat,
			SchemaFileNameFormat = FileNameFormat,
			FileExtension = FileExtension,
		};
}