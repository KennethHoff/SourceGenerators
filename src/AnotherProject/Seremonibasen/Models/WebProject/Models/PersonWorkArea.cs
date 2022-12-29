using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class PersonWorkArea
{

	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public required Guid Id { get; set; }

	public required Guid PersonId { get; set; }

	public required DateTime? LastCertificationDate { get; set; }
	public required int YearlyWorkRequirement { get; set; }
	public required bool HasSignedAgreement { get; set; }


	[ForeignKey("PersonId")]
	public virtual required Person Person { get; set; }
	public virtual required ICollection<County> Counties { get; set; }
	public virtual required ICollection<LocalArea> LocalAreas { get; set; }
	public virtual required ICollection<Season> Seasons { get; set; }

}