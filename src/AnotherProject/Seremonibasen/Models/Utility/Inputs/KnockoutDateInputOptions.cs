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