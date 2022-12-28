using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class NumberBuiltInAtomicZodSchema : IBuiltInAtomicZodSchema
{
	public SchemaDefinition SchemaDefinition => new("z.number()");
}