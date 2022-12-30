using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class FacilityPlace
{
	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	[StringLength(500)]
	public required string StreetAddress { get; set; }
	public required string PostCode { get; set; }
	[StringLength(512)]
	public required string City { get; set; }
	public required bool IsActive { get; set; }
	public required string Comment { get; set; }
	[StringLength(512)]
	public required string ContactName { get; set; }
	[StringLength(512)]
	public required string ContactMobile { get; set; }
	[StringLength(512)]
	public required string ContactEmail { get; set; }
		
	public required Guid? LocalAreaId { get; set; }
	[ForeignKey("LocalAreaId")]
	public required LocalArea LocalArea { get; set; }
	public required decimal Price { get; set; }


	public virtual required ICollection<FacilityRoom> FacilityRooms { get; set; }

	/// <summary>
	/// Table LocalAreasFacilityPlaces
	/// </summary>
	public virtual required ICollection<LocalArea> AvailableInLocalAreas { get; set; }
	public virtual required ICollection<FacilityPlaceType> FacilityPlaceTypes { get; set; }
}