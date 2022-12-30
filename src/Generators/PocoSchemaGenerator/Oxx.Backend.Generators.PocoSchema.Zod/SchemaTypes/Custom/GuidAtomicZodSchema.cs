using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

public class GuidAtomicZodSchema : IAtomicZodSchema
{
	public SchemaBaseName SchemaBaseName => new(Brand);
	public SchemaDefinition SchemaDefinition => new($"""z.string().uuid().brand<"{Brand}">()""");
	public virtual string Brand => "guid";
}