namespace AnotherProject.Seremonibasen.Models.Utility.Text;

[SchemaObject]
public class KnockoutFullAddressOptions
{
    public Localization Localization { get; set; }

    /// <summary>
    ///     Street Address
    /// </summary>
    public string TargetProperty { get; }

    /// <summary>
    ///     Zip Code
    /// </summary>
    public string TargetProperty2 { get; }

    /// <summary>
    ///     City
    /// </summary>
    public string TargetProperty3 { get; }

    public KnockoutFullAddressOptions(string targetProperty, string targetProperty2, string targetProperty3)
    {
        TargetProperty = targetProperty;
        TargetProperty2 = targetProperty2;
        TargetProperty3 = targetProperty3;
    }
}

