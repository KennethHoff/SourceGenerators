using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CourseDayParticipation
{
		
	public required Guid CourseDayId { get; set; }
	public required Guid PersonId { get; set; }

	[ForeignKey("CourseDayId")]
	public virtual required CourseDay CourseDay { get; set; }
	[ForeignKey("PersonId")]
	public virtual required Person Person { get; set; }
}