using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class BooleanZodSchemaType : IBuiltInZodSchemaType
{
	public string ValidationSchemaLogic => "z.boolean()";
	public string ValidationSchemaName => "boolean";
}