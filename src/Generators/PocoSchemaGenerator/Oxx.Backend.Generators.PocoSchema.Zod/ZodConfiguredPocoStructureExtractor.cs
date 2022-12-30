using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.Logic.PocoExtraction;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

internal sealed class ZodConfiguredPocoStructureExtractor : ConfiguredPocoStructureExtractor<ZodSchemaEvents>
{
	public ZodConfiguredPocoStructureExtractor(ISchemaConfiguration<ZodSchemaEvents> configuration)
		: base(configuration)
	{ }
}