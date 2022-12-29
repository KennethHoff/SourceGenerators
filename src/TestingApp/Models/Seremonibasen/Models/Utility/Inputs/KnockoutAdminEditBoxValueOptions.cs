using System.Linq.Expressions;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Inputs;

public class KnockoutAdminEditBoxValueOptions
{
    public Localization Localization { get; set; }
    public bool ReadOnly { get; set; } = false;
    public bool RenderHeader { get; set; } = true;

    public string TargetProperty { get; }

    public KnockoutAdminEditBoxValueOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }
}