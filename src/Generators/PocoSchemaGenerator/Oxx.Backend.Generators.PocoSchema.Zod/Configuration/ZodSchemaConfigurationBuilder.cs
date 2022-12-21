using Oxx.Backend.Generators.PocoSchema.Core.Configuration.Abstractions;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

namespace Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

public class ZodSchemaConfigurationBuilder : SchemaConfigurationBuilder<IZodSchemaType, ZodSchemaConfiguration, ZodSchemaEventConfiguration>
{
	public ZodSchemaConfigurationBuilder()
	{
		Substitute<string, StringZodSchemaType>();
		SubstituteIncludingNullable<int, NumberZodSchemaType>();
		SubstituteIncludingNullable<float, NumberZodSchemaType>();
		SubstituteIncludingNullable<double, NumberZodSchemaType>();
		SubstituteIncludingNullable<decimal, NumberZodSchemaType>();
		SubstituteIncludingNullable<Guid, GuidZodSchemaType>();
		SubstituteIncludingNullable<bool, BooleanZodSchemaType>();
		SubstituteIncludingNullable<DateTime, DateZodSchemaType>();
	}

	protected override ZodSchemaConfiguration CreateConfiguration()
		=> new()
		{
			SchemaTypeDictionary = SchemaTypeDictionary,
			Assemblies = Assemblies,
			OutputDirectory = OutputDirectory,
			PropertyNamingConvention = SchemaNamingConvention,
			PropertyTypeNamingConvention = SchemaTypeNamingConvention,
			DeleteFilesOnStart = DeleteFilesOnStart,
			Events = EventConfiguration ?? new ZodSchemaEventConfiguration(),
		};
}