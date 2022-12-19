using Oxx.Backend.Generators.PocoSchema.Zod.Core;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

public class GuidZodSchemaType : IZodSchemaType
{
	public SchemaLogic ValidationSchemaLogic => new("""z.string().uuid().brand<"GUID">()""");
	public BaseName ValidationSchemaName => new("guid");
}
