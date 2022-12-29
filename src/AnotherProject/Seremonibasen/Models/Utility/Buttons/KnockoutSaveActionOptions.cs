namespace AnotherProject.Seremonibasen.Models.Utility.Buttons;

[SchemaObject]
public class KnockoutSaveActionButtonOptions
{
    public int OptionValue { get; }
    public Localization Localization { get; set; }
    public string TargetProperty { get; }

    public SaveButtonType ButtonType { get; set; } = SaveButtonType.Light;

    protected KnockoutSaveActionButtonOptions(string targetProperty, int optionValue)
    {
        TargetProperty = targetProperty;
        OptionValue = optionValue;
    }
}

[SchemaEnum]
public enum SaveButtonType
{
    Dark,
    Text,
    Light,
}
