namespace AnotherProject.Seremonibasen.Models;

public readonly record struct Localization
{
    private string LocalizedText { get; }

    public static implicit operator string(Localization localization)
        => localization.LocalizedText;
}
