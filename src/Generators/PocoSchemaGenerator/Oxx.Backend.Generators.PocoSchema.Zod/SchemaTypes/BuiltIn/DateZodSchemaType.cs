using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.BuiltIn;

public class DateZodSchemaType : IBuiltInZodSchemaType
{
	public string ValidationSchemaLogic => "z.date()";
	public string ValidationSchemaName => "date";
}