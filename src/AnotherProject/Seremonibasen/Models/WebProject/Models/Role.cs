using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Role
{
	[Required]
	[StringLength(200)]
	public required string Name { get; set; }

	[InverseProperty(nameof(RoleApplicationRight.Role))]
	public virtual required ICollection<RoleApplicationRight> RoleApplicationRights { get; set; }
	public virtual required ICollection<Person> Persons { get; set; }
}