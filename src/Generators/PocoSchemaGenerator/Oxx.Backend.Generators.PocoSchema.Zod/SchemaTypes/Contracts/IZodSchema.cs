using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IZodSchema : IPartialZodSchema
{
	SchemaDefinition SchemaDefinition { get; }
}

public interface IPartialZodSchema : ISchema
{
	SchemaBaseName SchemaBaseName { get; }
}
