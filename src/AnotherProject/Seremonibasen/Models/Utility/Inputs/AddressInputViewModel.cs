namespace AnotherProject.Seremonibasen.Models.Utility.Inputs;

[SchemaObject]
public class AddressInputViewModel
{
    public Localization Localization { get; set; }

    public bool PostCodeRequired { get; set; }

    /// <summary>
    ///     Post Code
    /// </summary>
    public string TargetProperty { get; }

    /// <summary>
    ///     City
    /// </summary>
    public string TargetProperty2 { get; }

    /// <summary>
    ///     Street Address
    /// </summary>
    public string TargetProperty3 { get; }

    public AddressInputViewModel(string targetProperty, string targetProperty2, string targetProperty3)
    {
        TargetProperty = targetProperty;
        TargetProperty2 = targetProperty2;
        TargetProperty3 = targetProperty3;
    }
}