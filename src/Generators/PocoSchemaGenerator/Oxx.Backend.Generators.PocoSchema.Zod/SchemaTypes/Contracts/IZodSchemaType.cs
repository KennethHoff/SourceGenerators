using Oxx.Backend.Generators.PocoSchema.Core;
using Oxx.Backend.Generators.PocoSchema.Core.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IZodSchemaType : ISchemaType
{
	SchemaLogic ValidationSchemaLogic { get; }
	BaseName ValidationSchemaName { get; }
}

public record struct SchemaLogic(string Logic)
{
	public override string ToString()
		=> Logic;
}
