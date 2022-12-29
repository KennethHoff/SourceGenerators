namespace AnotherProject.Seremonibasen.Models.Utility.Links;

[SchemaObject]
public class KnockoutLinkOptions
{
    public Localization Localization { get; set; }
    public bool IncludeLabel { get; set; } = true;
    public string TargetProperty { get; }
    

    public KnockoutLinkOptions(string targetProperty, string url)
    {
        TargetProperty = targetProperty;
        Url = url;
    }

    public string Url { get; }
}
