using Oxx.Backend.Generators.PocoSchema.Zod.Core;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IZodSchemaType : ISchemaType
{
	string ValidationSchemaLogic { get; }
	string ValidationSchemaName { get; }
}