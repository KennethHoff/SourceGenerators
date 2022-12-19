using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class StringZodSchemaType : IBuiltInZodSchemaType
{
	public string ValidationSchemaLogic => "z.string()";
	public string ValidationSchemaName => "string";
}