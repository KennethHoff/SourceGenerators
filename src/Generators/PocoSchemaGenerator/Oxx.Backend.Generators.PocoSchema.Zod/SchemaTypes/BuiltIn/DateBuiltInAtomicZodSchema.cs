using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class DateBuiltInAtomicZodSchema : IBuiltInAtomicZodSchema
{
	public SchemaDefinition SchemaDefinition => new("z.date()");
}