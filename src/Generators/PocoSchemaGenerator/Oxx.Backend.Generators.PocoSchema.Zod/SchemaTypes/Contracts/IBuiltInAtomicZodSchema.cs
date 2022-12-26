using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

/// <summary>
/// A built-in atomic schema is a schema that is defined by it's type definition.
/// That is to say, it is not referenced by other schemas, but rather used directly.
/// Therefore, the name of the schema is the same as the type definition
/// </summary>
/// <example>in Zod, a string is defined by `z.string()`,
/// and instead of having a reference to "stringSchema" everywhere, which feels weird", just use `z.string()` everywhere.</example>
public interface IBuiltInAtomicZodSchema : IAtomicZodSchema
{
	SchemaBaseName IPartialZodSchema.SchemaBaseName => new(SchemaDefinition);
}