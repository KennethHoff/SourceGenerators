using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.FileCreation;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGenerator : SchemaGenerator<ZodSchemaEvents>
{
	public ZodSchemaGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<ZodSchemaEvents> configuration)
		: base(schemaConverter, configuration, CreatePocoStructureExtractor(configuration), CreateSchemaExtractor(configuration))
	{ }

	private static IPocoStructureExtractor CreatePocoStructureExtractor(ISchemaConfiguration<ZodSchemaEvents> configuration)
		=> new ZodConfiguredPocoStructureExtractor(configuration);

	private static ISchemaFileCreator CreateSchemaExtractor(ISchemaConfiguration<ZodSchemaEvents> configuration)
		=> new ZodSchemaFileCreator(configuration);
}