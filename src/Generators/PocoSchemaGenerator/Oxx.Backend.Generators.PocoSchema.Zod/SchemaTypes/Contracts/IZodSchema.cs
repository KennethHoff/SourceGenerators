using Oxx.Backend.Generators.PocoSchema.Core.Models.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IZodSchema : ISchema
{
	SchemaName SchemaName { get; }
}
