using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

/// <summary>
/// Honorar
/// </summary>
[SchemaObject]
[Table("Remunerations")]
public class Remuneration
{
	public required string Description { get; set; }
	public required DateTime? ExportedDate { get; set; }
	public required DateTime? ApprovedDate { get; set; }

	public required Guid PersonId { get; set; }
	public virtual required Person Person { get; set; }

	public required Guid? SeasonId { get; set; }
	[ForeignKey(nameof(SeasonId))]
	public virtual required Season Season { get; set; }

	public required Guid? CountyId { get; set; }
	[ForeignKey(nameof(CountyId))]
	public virtual required County County { get; set; }

	public required Guid? LocalAreaId { get; set; }
	[ForeignKey(nameof(LocalAreaId))]
	public virtual required LocalArea LocalArea { get; set; }

	public required Guid? ApprovedById { get; set; }
	public virtual required Person ApprovedBy { get; set; }

	public required Guid? ExportedById { get; set; }
	public virtual required Person ExportedBy { get; set; }
	public required Guid? CourseId { get; set; }
	public virtual required Course Course { get; set; }
	public required Guid? CeremonyId { get; set; }
	public virtual required Ceremony Ceremony { get; set; }

	public required Guid? CeremonyTypeId { get; set; }

	public virtual required ICollection<RemunerationRate> RemunerationRates { get; set; }
}