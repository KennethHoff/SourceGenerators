using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace TestingApp.SchemaTypes;

internal sealed class ClampedNumberAtomicZodSchema : IAtomicZodSchema
{
	public SchemaBaseName SchemaBaseName => new("ClampedNumber");
	public SchemaDefinition SchemaDefinition => new("z.number().min(0).max(100)");
}

public readonly record struct ClampedNumber(int Value)
{
	public int Value { get; init; } = Value < 0 ? 0 : Value > 100 ? 100 : Value;
}