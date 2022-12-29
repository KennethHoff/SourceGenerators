using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrderForm
{
	[Required]
	[StringLength(500)]
	public string? Name { get; set; }
	public string? Description { get; set; }
	public bool IsDefault { get; set; }
	public bool IsActive { get; set; }
	public bool IsMarriageRegisterRequired { get; set; }

	[Column(TypeName = "money")]
	public decimal Price { get; set; }

	[Column(TypeName = "money")]
	public decimal PriceMember { get; set; }

	public Guid CeremonyTypeId { get; set; }
        
	[StringLength(500)]
	public string? ContractName { get; set; }
        
	public string? ContractDescription { get; set; }

	public virtual ICollection<CeremonyOrder>? CeremonyOrders { get; set; }
}