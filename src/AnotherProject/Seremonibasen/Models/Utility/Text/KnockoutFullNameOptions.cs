namespace AnotherProject.Seremonibasen.Models.Utility.Text;

[SchemaObject]
public class KnockoutFullNameOptions
{
    public Localization Localization { get; set; }

    /// <summary>
    ///     First Name
    /// </summary>
    public string TargetProperty { get; }

    /// <summary>
    ///     Surname
    /// </summary>
    public string TargetProperty2 { get; }

    public KnockoutFullNameOptions(string targetProperty, string targetProperty2)
    {
        TargetProperty = targetProperty;
        TargetProperty2 = targetProperty2;
    }
}
