using TestingApp.Models.Seremonibasen.Models.Abstractions;

namespace TestingApp.Models.Seremonibasen.Models.Remuneration;

[SchemaObject]
public class PersonRemuneration : DeletablePocoObject
{
    public Guid PersonId { get; set; }
    public string? Role { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid RemunerationId { get; set; }
    public List<PersonContractRate> RatesFromContract { get; set; } = new();
    public List<PersonRemunerationRate> RemunerationRates { get; set; } = new();
    public bool SpeakerDoNotWantRate { get; set; }
    public bool CeremonyWasLongerThanEightHours { get; set; }
}
