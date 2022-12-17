namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

public sealed class StringZodSchemaType : IZodSchemaType
{
	public string ValidationSchemaLogic => "z.string()";
	public string ValidationSchemaName => "stringSchema";
}
