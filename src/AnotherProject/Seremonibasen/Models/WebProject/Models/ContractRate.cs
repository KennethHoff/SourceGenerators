using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class ContractRate : ICloneable
{
	public required int SortIndex { get; set; }

	[Column(TypeName = "money")]
	public required decimal Amount { get; set; }

	public required string Description { get; set; }

	public required bool IsLocked { get; set; }

	public required Guid? SeasonId { get; set; }
	[ForeignKey("SeasonId")]
	public required Season Season { get; set; }

	public required Guid? CountyId { get; set; }
	[ForeignKey("CountyId")]
	public required County County { get; set; }

	public required Guid? LocalAreaId { get; set; }
	[ForeignKey("LocalAreaId")]
	public required LocalArea LocalArea { get; set; }

	public required Guid? CourseTypeId { get; set; }
	[ForeignKey("CourseTypeId")]
	public virtual required CourseType CourseType { get; set; }

	public required Guid? ActivityId { get; set; }
	[ForeignKey("ActivityId")]
	public virtual required Activity Activity { get; set; }

	public required Guid? OverrideContractRateId { get; set; }
	[ForeignKey("OverrideContractRateId")]
	public required ContractRate OverrideContractRate { get; set; }

	/// <summary>
	/// ID of the "template" that this contract rate is based on.
	/// </summary>
	public required Guid? CopyFromContractRateId { get; set; }

	public required Guid? CeremonyTypeId { get; set; }
	[ForeignKey("CeremonyTypeId")]

	public required Guid? ContractId { get; set; }
	[ForeignKey("ContractId")]
	public required Contract Contract { get; set; }

	public required Guid? CourseId { get; set; }
	[ForeignKey("CourseId")]
	public required Course Course { get; set; }

	public required Guid? CeremonyOrderId { get; set; }
	[ForeignKey("CeremonyOrderId")]
	public virtual required CeremonyOrder CeremonyOrder { get; set; }

	public object Clone()
	{
		return MemberwiseClone();
	}
}