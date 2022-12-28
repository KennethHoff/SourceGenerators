using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IBuiltInZodSchema : IZodSchema
{
	SchemaBaseName IPartialZodSchema.SchemaBaseName => new(SchemaDefinition);
}