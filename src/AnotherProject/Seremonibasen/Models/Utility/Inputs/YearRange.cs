namespace AnotherProject.Seremonibasen.Models.Utility.Inputs;

[SchemaObject]
public readonly record struct YearRange
{
	public bool IsSet => Min != 0 || Max != 0;
	public int Max { get; }
	public int Min { get; }

	public YearRange(int min, int max)
	{
		Min = min;
		Max = max == 0 ? 9999 : max;
	}
}