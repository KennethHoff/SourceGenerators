using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Activity
{
	public required Guid? OriginalId { get; set; }


	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	[Column(TypeName = "money")]
	public required decimal Price { get; set; }
	public required string Description { get; set; }

	public required Guid SeasonId { get; set; }

	[ForeignKey(nameof(SeasonId))]
	public virtual required Season Season { get; set; }

	public required Guid? CountyId { get; set; }

	[ForeignKey(nameof(CountyId))]
	public virtual required County County { get; set; }
        
	public virtual required ICollection<LocalArea> AvailableInLocalAreas { get; set; }
	public virtual required ICollection<Course> Courses { get; set; }

	public virtual required ICollection<Course> CancelledForCourses { get; set; }
        
	public Activity CreateResultData()
	{
		return this;
	}
}