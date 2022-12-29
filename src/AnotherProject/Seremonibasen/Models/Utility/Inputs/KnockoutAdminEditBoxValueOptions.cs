namespace AnotherProject.Seremonibasen.Models.Utility.Inputs;

[SchemaObject]
public class KnockoutAdminEditBoxValueOptions
{
    public Localization Localization { get; set; }
    public bool ReadOnly { get; set; } = false;
    public bool RenderHeader { get; set; } = true;

    public string TargetProperty { get; }

    public KnockoutAdminEditBoxValueOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }
}