using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrderForm
{
	[Required]
	[StringLength(500)]
	public required string? Name { get; set; }
	public required string? Description { get; set; }
	public required bool IsDefault { get; set; }
	public required bool IsActive { get; set; }
	public required bool IsMarriageRegisterRequired { get; set; }

	[Column(TypeName = "money")]
	public required decimal Price { get; set; }

	[Column(TypeName = "money")]
	public required decimal PriceMember { get; set; }

	public required Guid CeremonyTypeId { get; set; }
        
	[StringLength(500)]
	public required string? ContractName { get; set; }
        
	public required string? ContractDescription { get; set; }

	public virtual required ICollection<CeremonyOrder>? CeremonyOrders { get; set; }
}