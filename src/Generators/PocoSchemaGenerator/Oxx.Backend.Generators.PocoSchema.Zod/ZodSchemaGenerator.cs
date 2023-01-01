using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.FileCreation;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGenerator : SchemaGenerator<ZodSchemaEvents, ZodDirectoryOutputConfiguration>
{
	public ZodSchemaGenerator(
		ZodSchemaConfiguration configuration,
		ISchemaConverter? schemaConverter = null,
		ISchemaFileCreator? schemaFileCreator = null,
		IPocoStructureExtractor? pocoStructureExtractor = null)
		: base(configuration,
			schemaConverter ?? CreateSchemaConverter(configuration),
			pocoStructureExtractor ?? CreatePocoStructureExtractor(configuration),
			schemaFileCreator ?? CreateSchemaFileCreator(configuration))
	{ }

	private static IPocoStructureExtractor CreatePocoStructureExtractor(ZodSchemaConfiguration configuration)
		=> new ZodConfiguredPocoStructureExtractor(configuration);

	private static ISchemaFileCreator CreateSchemaFileCreator(ISchemaConfiguration<ZodSchemaEvents, ZodDirectoryOutputConfiguration> configuration)
		=> new ZodSchemaFileCreator(configuration);

	private static ISchemaConverter CreateSchemaConverter(ZodSchemaConfiguration configuration)
		=> new ZodSchemaConverter(configuration);
}