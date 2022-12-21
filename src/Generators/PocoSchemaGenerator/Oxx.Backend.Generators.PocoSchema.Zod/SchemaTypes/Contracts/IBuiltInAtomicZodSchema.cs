namespace Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

public interface IBuiltInAtomicZodSchema : IAtomicZodSchema
{
	SchemaName IZodSchema.SchemaName => SchemaName.BuiltIn;
}
