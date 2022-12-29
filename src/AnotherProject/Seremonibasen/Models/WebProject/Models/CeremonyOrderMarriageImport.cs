using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrderMarriageImport
{
    public required Guid CeremonyOrderId { get; set; }
    [ForeignKey(nameof(CeremonyOrderId))]
    public virtual required CeremonyOrder Order { get; set; }

    public required int ExternalOrderNumber { get; set; }
    public required int? ExternalVenueId { get; set; }
    public required int? ExternalEmployeeId { get; set; }

    public required DateTime ImportedDateTime { get; set; }
    public required bool IsInvoiced { get; set; }
    public required bool IsStarted { get; set; }
    public required bool IsEmployeeFound { get; set; }

    public required bool IsVenueFound { get; set; }
    public required string? VenueDescription { get; set; }


    public required Guid? ApprovedByEmployeeId { get; set; }
    [ForeignKey(nameof(ApprovedByEmployeeId))]
    public virtual required Person? ApprovedByEmployee { get; set; }
}
