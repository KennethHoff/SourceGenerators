using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IAtomicZodSchema, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	public ZodSchemaConfigurationBuilder()
	{
		Substitute<string, StringAtomicZodSchema>();
		SubstituteIncludingNullable<int, NumberAtomicZodSchema>();
		SubstituteIncludingNullable<float, NumberAtomicZodSchema>();
		SubstituteIncludingNullable<double, NumberAtomicZodSchema>();
		SubstituteIncludingNullable<decimal, NumberAtomicZodSchema>();
		SubstituteIncludingNullable<Guid, GuidAtomicZodSchema>();
		SubstituteIncludingNullable<bool, BooleanAtomicZodSchema>();
		SubstituteIncludingNullable<DateTime, DateAtomicZodSchema>();
	}

	protected override string SchemaNamingFormat { get; set; } = "{0}Schema";
	protected override string SchemaTypeNamingFormat { get; set; } = "{0}SchemaType";
	protected override string SchemaFileNameFormat { get; set; } = "{0}Schema.ts";

	protected override ZodSchemaConfiguration CreateConfiguration()
		=> new()
		{
			AtomicSchemaDictionary = SchemaTypeDictionary,
			Assemblies = Assemblies,
			OutputDirectory = OutputDirectory,
			PropertyNamingFormat = SchemaNamingFormat,
			DeleteFilesOnStart = DeleteFilesOnStart,
			Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
			SchemaNamingFormat = SchemaNamingFormat,
			SchemaTypeNamingFormat = SchemaTypeNamingFormat,
			SchemaFileNameFormat = SchemaFileNameFormat,
		};
}