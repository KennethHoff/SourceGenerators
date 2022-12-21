using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

public class GuidAtomicZodSchema : IAtomicZodSchema
{
	public SchemaDefinition SchemaDefinition => new("""z.string().uuid().brand<"GUID">()""");
	public SchemaBaseName SchemaBaseName => new("guid");
}