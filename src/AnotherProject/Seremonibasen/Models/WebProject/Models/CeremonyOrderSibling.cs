using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrderSibling
{
		
	public required Guid CeremonyOrderId { get; set; }
	public required Guid? SiblingCeremonyOrderId { get; set; }
	public required string FirstName { get; set; }
	public required string Surname { get; set; }

	[ForeignKey("CeremonyOrderId")]
	public virtual required CeremonyOrder CeremonyOrder { get; set; }

	[ForeignKey("SiblingCeremonyOrderId")]
	public virtual required CeremonyOrder SiblingCeremonyOrder { get; set; }

}