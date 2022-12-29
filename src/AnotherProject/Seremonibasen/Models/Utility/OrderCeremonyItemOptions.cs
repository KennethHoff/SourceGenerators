namespace AnotherProject.Seremonibasen.Models.Utility;

[SchemaObject]
public class OrderCeremonyItemOptions
{
    public string? PropertyName { get; set; }
    public Localization TitleLocalization { get; set; }
    public bool ShowRemoveButton { get; set;}
    public bool ShowUrl { get; set; }
}
