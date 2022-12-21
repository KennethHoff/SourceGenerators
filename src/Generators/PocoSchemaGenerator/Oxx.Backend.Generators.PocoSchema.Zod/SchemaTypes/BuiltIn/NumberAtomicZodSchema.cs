using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class NumberAtomicZodSchema : IBuiltInAtomicZodSchema
{
	public SchemaDefinition SchemaDefinition => new("z.number()");
}