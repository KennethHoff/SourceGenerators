namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

public sealed class NumberZodSchemaType : IZodSchemaType
{
	public string ValidationSchemaLogic => "z.number()";
	public string ValidationSchemaName => "numberSchema";
}
