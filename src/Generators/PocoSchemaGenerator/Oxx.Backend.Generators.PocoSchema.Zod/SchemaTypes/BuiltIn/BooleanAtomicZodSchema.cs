using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class BooleanAtomicZodSchema : IBuiltInAtomicZodSchema
{
	public SchemaDefinition SchemaDefinition => new("z.boolean()");
}