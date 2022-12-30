namespace TestingApp.SchemaTypes;

public readonly record struct ClampedNumber(int Value)
{
	public int Value { get; init; } = Value < 0 ? 0 : Value > 100 ? 100 : Value;
}