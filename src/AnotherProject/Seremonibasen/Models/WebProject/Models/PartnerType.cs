using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
[Table("PartnerTypes")]
public class PartnerType
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public required Guid Id { get; set; }
	public required bool IsDeleted { get; set; }
	public required DateTime ModifiedDate { get; set; }
	public required Guid? ModifiedById { get; set; }
	public required DateTime? CreatedDate { get; set; }
	public required Guid? CreatedById { get; set; }

	[Required]
	[StringLength(50)]
	public required string Name { get; set; }


	public virtual required ICollection<Partner> Partners { get; set; }
       
}