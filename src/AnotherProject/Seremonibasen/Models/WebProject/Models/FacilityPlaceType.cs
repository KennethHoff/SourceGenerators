using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class FacilityPlaceType
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }


	public virtual required ICollection<FacilityPlace> FacilityPlaces { get; set; }
}