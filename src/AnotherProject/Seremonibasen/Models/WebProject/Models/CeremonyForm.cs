using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyForm
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }

	public required string Description { get; set; }
	public required bool IsDefault { get; set; }
	public required bool IsActive { get; set; }
	public required Guid CeremonyTypeId { get; set; }
	[ForeignKey("CeremonyTypeId")]
	public virtual required ICollection<Ceremony> Ceremonies { get; set; }
}