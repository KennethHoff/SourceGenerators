using Oxx.Backend.Generators.PocoSchema.Zod.Core;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

public interface IZodSchemaType : ISchemaType
{
	string ValidationSchemaLogic { get; }
	string ValidationSchemaName { get; }
}