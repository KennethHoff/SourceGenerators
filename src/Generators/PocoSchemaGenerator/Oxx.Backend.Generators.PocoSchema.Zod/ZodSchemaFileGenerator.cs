using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Core.PocoExtractors;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaFileGenerator : SchemaFileGenerator<IPartialZodSchema, ZodSchemaEventConfiguration>
{
	public ZodSchemaFileGenerator(ISchemaConverter schemaConverter, ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		: base(schemaConverter, configuration, CreatePocoStructureExtractor(configuration))
	{ }
	
	private static IPocoStructureExtractor CreatePocoStructureExtractor(ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		=> new ZodAssemblyPocoStructureExtractor(configuration.Assemblies, configuration);
}
