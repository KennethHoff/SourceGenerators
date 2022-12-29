using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
[Table("CeremonyLeaderOptions")]
public class CeremonyLeaderOption
{
	[Key]
	[Column(Order = 0)]
	public required Guid CeremonyId { get; set; }
	[Key]
	[Column(Order = 1)]
	public required Guid PersonId { get; set; }

	[ForeignKey("CeremonyId")]
	public virtual required Ceremony Ceremony { get; set; }
	[ForeignKey("PersonId")]
	public virtual required Person Person { get; set; }
}