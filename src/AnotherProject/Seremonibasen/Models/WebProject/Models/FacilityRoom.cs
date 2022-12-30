using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class FacilityRoom
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	public required int NumberOfSeats { get; set; }
	public required Guid? FacilityPlaceId { get; set; }
	public required bool IsActive { get; set; }
	public required bool UseNameFromFacilityPlace { get; set; }
	public required string Comment { get; set; }


	[ForeignKey("FacilityPlaceId")]
	public virtual required FacilityPlace? FacilityPlace { get; set; }
	public virtual required ICollection<Ceremony> Ceremonies { get; set; }
	public virtual required ICollection<Course> Courses { get; set; }

}