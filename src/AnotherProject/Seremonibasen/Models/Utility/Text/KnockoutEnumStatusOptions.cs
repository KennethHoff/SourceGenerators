namespace AnotherProject.Seremonibasen.Models.Utility.Text;

[SchemaObject]
public class KnockoutEnumStatusOptions
{
    public Localization Localization { get; set; }
    public required string TargetProperty { get; set; }


    public IReadOnlyCollection<string> UnsetValues { get; init; } = new []{ "0" }; // The default value for an enum is 0, so if it's set to that value, it's not set.

}
