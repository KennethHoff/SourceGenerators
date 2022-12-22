using Oxx.Backend.Generators.PocoSchema.Zod.SchemaTypes.Contracts;

namespace TestingApp.SchemaTypes;

internal sealed class ClampedNumberAtomicZodSchema : IAtomicZodSchema
{
	private readonly Range _range;

	public ClampedNumberAtomicZodSchema()
	{
		_range = ..100;
	}
	public ClampedNumberAtomicZodSchema(Range range)
	{
		_range = range;
	}
	public SchemaBaseName SchemaBaseName => new("ClampedNumber");
	public SchemaDefinition SchemaDefinition => new($"z.number().min({_range.Start}).max({_range.End})");
}

public readonly record struct ClampedNumber(int Value)
{
	public int Value { get; init; } = Value < 0 ? 0 : Value > 100 ? 100 : Value;
}