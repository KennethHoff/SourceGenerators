using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.PocoExtractors;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGenerator : SchemaGenerator<IPartialZodSchema, ZodSchemaEventConfiguration>
{
	public ZodSchemaGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		: base(schemaConverter, configuration, CreatePocoStructureExtractor(configuration), CreateSchemaExtractor(configuration))
	{ }

	private static IPocoStructureExtractor CreatePocoStructureExtractor(ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		=> new ZodAssemblyPocoStructureExtractor(configuration.Assemblies, configuration);

	private static ISchemaFileCreator CreateSchemaExtractor(ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		=> new ZodSchemaFileCreator(configuration);
}