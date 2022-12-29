namespace AnotherProject.Seremonibasen.Models.Utility.Checkboxes;

[SchemaObject]
public class KnockoutCheckboxOptions
{
    public KnockoutCheckboxOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }

    public bool Disabled { get; set; } = false;
    public Localization Localization { get; set; }
    public string TargetProperty { get;  }
    public bool RenderLabel { get; set; } = true;
}