using System.Linq.Expressions;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Text;

public abstract class KnockoutSsnOptions
{
    public Localization Localization { get; set; }
    public string SsnProperty { get; }

    /// <summary>
    /// Will be rendered next to the label
    /// </summary>
    public Localization? AdditionalInformation { get; set; }

    /// <summary>
    /// HasSSN
    /// </summary>
    public string TargetProperty { get; }

    protected KnockoutSsnOptions(string targetProperty, string ssnProperty)
    {
        TargetProperty = targetProperty;
        SsnProperty = ssnProperty;
    }
}

