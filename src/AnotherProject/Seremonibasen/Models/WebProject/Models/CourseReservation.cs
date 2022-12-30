using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CourseReservation
{
	[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public required Guid Id { get; set; }
	public required Guid CourseId { get; set; }
	public required DateTime ExpirationDate { get; set; }
	public required bool IsWaitingList { get; set; }

	[ForeignKey("CourseId")]
	public virtual required Course Course { get; set; }
}