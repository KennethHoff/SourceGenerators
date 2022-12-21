using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.Configuration;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod;

public sealed class ZodSchemaGenerator : SchemaGenerator<IZodSchemaType, ZodSchemaEventConfiguration>
{
	public ZodSchemaGenerator(ISchema zodSchema, ZodSchemaConfiguration configuration)
		: base(zodSchema, configuration)
	{ }
}
