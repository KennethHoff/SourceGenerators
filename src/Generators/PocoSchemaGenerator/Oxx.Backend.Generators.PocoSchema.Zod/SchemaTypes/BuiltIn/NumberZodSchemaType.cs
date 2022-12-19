using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class NumberZodSchemaType : IBuiltInZodSchemaType
{
	public string ValidationSchemaLogic => "z.number()";
	public string ValidationSchemaName => "number";
}