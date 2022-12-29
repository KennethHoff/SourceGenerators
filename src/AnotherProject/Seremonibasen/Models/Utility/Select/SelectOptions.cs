namespace AnotherProject.Seremonibasen.Models.Utility.Select;

public struct SelectOptions<TEnum> where TEnum : Enum
{
    public SelectOptions()
    { }

    /// <summary>
    /// KnockoutJS Property used for disabling certain Options in a Select
    /// </summary>
    public string? DisabledElementsProperty { get; set; } = null;

    /// <summary>
    /// Enum values to be hidden in the Select
    /// </summary>
    public IEnumerable<TEnum>? HiddenOptions { get; set; } = null;

    /// <summary>
    /// Default option used as e.g. `All` when it comes to filtering
    /// </summary>
    public string? AdditionalDefaultString { get; set; } = null;

    public bool OnlyRenderWrapperDiv { get; set; } = false;
}

