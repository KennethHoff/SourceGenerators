using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrderPerson
{
	[Key]
	[Column(Order = 0)]
	public required Guid CeremonyOrderId { get; set; }
	[Key]
	[Column(Order = 1)]
	public required Guid PersonId { get; set; }

	[ForeignKey("CeremonyOrderId")]
	public virtual required CeremonyOrder CeremonyOrder { get; set; }
	[ForeignKey("PersonId")]
	public virtual required Person Person { get; set; }

}