using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class School
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	public required Guid CountyId { get; set; }
	public required bool Approved { get; set; }


	[ForeignKey("CountyId")]
	public virtual required County County { get; set; }
}