namespace TestingApp.Models.Seremonibasen.Models.Remuneration;

[SchemaObject]
public class PersonContractRate
{
    public Guid Id { get; init; }
    public string? Description { get; init; }
    public decimal Amount { get; init; }
}
