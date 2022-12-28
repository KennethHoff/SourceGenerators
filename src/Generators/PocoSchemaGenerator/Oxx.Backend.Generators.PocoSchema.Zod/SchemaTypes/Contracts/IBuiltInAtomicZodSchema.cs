using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IBuiltInAtomicZodSchema : IAtomicZodSchema
{
	SchemaBaseName IPartialZodSchema.SchemaBaseName => new(SchemaDefinition);
}