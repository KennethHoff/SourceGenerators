using System.Reflection;
using Oxx.Backend.Generators.PocoSchema.Core.Configuration;
using Oxx.Backend.Generators.PocoSchema.Core.PocoExtractors;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

internal sealed class ZodAssemblyPocoStructureExtractor : AssemblyPocoStructureExtractor<IPartialZodSchema, ZodSchemaEventConfiguration>
{
	public ZodAssemblyPocoStructureExtractor(
		IReadOnlyCollection<Assembly> assemblies,
		ISchemaConfiguration<IPartialZodSchema, ZodSchemaEventConfiguration> configuration)
		: base(assemblies, configuration)
	{ }
}

