using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class RoleApplicationRight
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public required Guid Id { get; set; }
	public required Guid RoleId { get; set; }


	[ForeignKey("RoleId")]
	public virtual required Role Role { get; set; }
}