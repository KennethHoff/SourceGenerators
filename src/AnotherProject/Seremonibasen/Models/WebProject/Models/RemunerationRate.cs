using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

/// <summary>
/// Honorar sats
/// </summary>
[Table("RemunerationRates")]
[SchemaObject]
public class RemunerationRate
{
	public required int SortIndex { get; set; }
	public required int Quantity { get; set; }
	[Column(TypeName = "money")]
	public required decimal Amount { get; set; }
	public required string Description { get; set; }


	public required Guid RemunerationId { get; set; }
	[ForeignKey(nameof(RemunerationId))]
	public virtual required Remuneration Remuneration { get; set; }

	public required Guid? ContractRateId { get; set; }
	[ForeignKey(nameof(ContractRateId))]
	public virtual required ContractRate ContractRate { get; set; }

	public required Guid? ContractId { get; set; }
	[ForeignKey(nameof(ContractId))]
	public virtual required Contract Contract { get; set; }

	public required Guid? CeremonyTypeId { get; set; }

	public required Guid? CeremonyId { get; set; }
	[ForeignKey(nameof(CeremonyId))]
	public virtual required Ceremony Ceremony { get; set; }

	public required Guid? CourseId { get; set; }
	[ForeignKey(nameof(CourseId))]
	public virtual required Course Course { get; set; }

	public required Guid? CourseTypeId { get; set; }
	[ForeignKey(nameof(CourseTypeId))]
	public virtual required CourseType CourseType { get; set; }

	public required Guid? ActivityId { get; set; }
	[ForeignKey(nameof(ActivityId))]
	public virtual required Activity Activity { get; set; }
}