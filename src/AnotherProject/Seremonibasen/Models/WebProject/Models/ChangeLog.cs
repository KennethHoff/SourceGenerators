using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class ChangeLog
{
	public required Guid ModifiedId { get; set; }

        
	public required string DeltaChanges { get; set; }


	[ForeignKey("ModifiedById")]
	public virtual required Person ModifiedBy { get; set; }
}