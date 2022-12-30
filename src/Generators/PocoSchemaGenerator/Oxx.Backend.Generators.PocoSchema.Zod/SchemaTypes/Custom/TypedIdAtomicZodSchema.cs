namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Custom;

public class TypedIdAtomicZodSchema<TTypedId> : GuidAtomicZodSchema
{
	public override string Brand => typeof(TTypedId).Name;
}