namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes;

public sealed class GuidZodSchemaType : IZodSchemaType
{
	public string ValidationSchemaLogic => """z.string().uuid().brand<"GUID">""";
	public string ValidationSchemaName => "guidSchema";
}
