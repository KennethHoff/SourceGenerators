namespace AnotherProject.Seremonibasen.Models.Razor;

[SchemaObject]
public class CeremonyOrderCompletionRazorModel
{
    public Localization SpeakerLocalization { get; init; }
    public Localization SpeakerSignedLocalization { get; init; }
    public Localization NotSignedLocalization { get; init; }
    public Localization OrSignedLocalization { get; init; }
}
