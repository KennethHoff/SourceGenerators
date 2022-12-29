namespace AnotherProject.Seremonibasen.Models.Utility.Inputs;

[SchemaObject]
public class KnockoutDateInputOptions
{
    public bool IncludeTime { get; set; }
    public InputOptions InputOptions { get; set; } = new();
    public Localization Localization { get; set; }
    public string TargetProperty { get; }
    public YearRange YearRange { get; set; }

    public KnockoutDateInputOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }
}

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
