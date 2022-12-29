using System.Linq.Expressions;
using TestingApp.Models.Seremonibasen.Models.Structs;

namespace TestingApp.Models.Seremonibasen.Models.Utility.Text;

public class KnockoutBoxOptions
{
    public string HiddenProperty { get; set; } = string.Empty;
    public Localization Localization { get; set; }
    public bool ReadOnly { get; set; } = false;
    public bool RenderHeader { get; set; } = true;

    public string TargetProperty { get; }

    public KnockoutBoxOptions(string targetProperty)
    {
        TargetProperty = targetProperty;
    }
}
