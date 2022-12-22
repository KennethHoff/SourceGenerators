using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGenerator : SchemaGenerator<IAtomicZodSchema, ZodSchemaEventConfiguration>
{
	public ZodSchemaGenerator(ISchemaConverter zodSchemaConverter, ZodSchemaConfiguration configuration)
		: base(zodSchemaConverter, configuration)
	{ }
}
