namespace TestingApp.Models.Seremonibasen.Models.Utility.Labels;

public struct StandardLabelOptions
{
    public StandardLabelOptions()
    { }

    public static StandardLabelOptions NoLabel => new()
    {
        ShouldRenderLabel = false,
    };

    public bool ShouldRenderLabel { get; set; } = true;
    public Localization Localization { get; set; } = new();
}
