using AnotherProject.Seremonibasen.Models.Abstractions;

namespace AnotherProject.Seremonibasen.Models.Remuneration;

[SchemaObject]
public class PersonRemunerationRate : DeletablePocoObject
{
    public Guid? ContractRateId { get; init; }
    public Guid? ContractId { get; init; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; init; }
}
