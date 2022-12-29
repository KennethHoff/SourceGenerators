using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CourseType
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	public required bool IsDefault { get; set; }
	public required bool IsActive { get; set; }
	public required int SortIndex { get; set; }
	public virtual required ICollection<Course> Courses { get; set; }
}