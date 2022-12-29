using System.Linq.Expressions;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Links;

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
