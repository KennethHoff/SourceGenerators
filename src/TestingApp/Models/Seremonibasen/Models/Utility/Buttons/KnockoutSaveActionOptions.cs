using System.Linq.Expressions;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Buttons;

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

public enum SaveButtonType
{
    Dark,
    Text,
    Light,
}
