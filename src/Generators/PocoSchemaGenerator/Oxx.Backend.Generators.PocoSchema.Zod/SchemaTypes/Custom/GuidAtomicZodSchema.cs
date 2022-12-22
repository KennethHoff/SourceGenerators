using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;
using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts.Models;

namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

public class GuidAtomicZodSchema : IAtomicZodSchema
{
	public virtual string Brand => "guid";
	public SchemaDefinition SchemaDefinition => new($"""z.string().uuid().brand<"{Brand}">()""");
	public SchemaBaseName SchemaBaseName => new(Brand);
}
public class TypedIdAtomicZodSchema<TTypedId> : GuidAtomicZodSchema
{
	public override string Brand => typeof(TTypedId).Name;
}