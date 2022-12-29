using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class LocalArea
{
	public LocalArea(string name, string shortName, string legalName, string validZipCodes, string municipal, string municipalCode, string organizationNumber, County county, ICollection<Ceremony> ceremonies, ICollection<Course> courses, ICollection<PersonWorkArea> personWorkAreas)
	{
		Name = name;
		ShortName = shortName;
		LegalName = legalName;
		ValidZipCodes = validZipCodes;
		Municipal = municipal;
		MunicipalCode = municipalCode;
		OrganizationNumber = organizationNumber;
		County = county;
		Ceremonies = ceremonies;
		Courses = courses;
		PersonWorkAreas = personWorkAreas;
		LocalAreaSettings = new List<LocalAreaSettings>();
		ForSaleCeremonies = new List<Ceremony>();
		ForSaleCourses = new List<Course>();
		FacilityPlaces = new List<FacilityPlace>();
		AvailableFacilityPlaces = new List<FacilityPlace>();
		AvailableActivites = new List<Activity>();
		CeremonyOrders = new List<CeremonyOrder>();
		AccessibleByPersons = new List<Person>();
		AccessToSignContracts = new List<Person>();
	}

	[Required]
	[StringLength(500)]
	public string Name { get; set; }
	[StringLength(50)]
	public string ShortName { get; set; }
	[StringLength(500)]
	public string LegalName { get; set; }

	public int? PrisolveId { get; set; }

	public Guid? CountyId { get; set; }

	public string ValidZipCodes { get; set; }

	public string Municipal { get; set; }
	public string MunicipalCode { get; set; }
	public string OrganizationNumber { get; set; }

	[ForeignKey(nameof(CountyId))]
	public virtual County County { get; set; }

	public virtual ICollection<LocalAreaSettings> LocalAreaSettings { get; set; }

	[InverseProperty(nameof(Ceremony.LocalArea))]
	public virtual ICollection<Ceremony> Ceremonies { get; set; }

	[InverseProperty(nameof(Course.LocalArea))]
	public virtual ICollection<Course> Courses { get; set; }

	public virtual ICollection<Person> AccessibleByPersons { get; set; }
	public virtual ICollection<Activity> AvailableActivites { get; set; }

	/// <summary>
	/// Table LocalAreaCeremonies
	/// </summary>
	public virtual ICollection<Ceremony> ForSaleCeremonies { get; set; }

	/// <summary>
	/// Table LocalAreaCourses
	/// </summary>
	public virtual ICollection<Course> ForSaleCourses { get; set; }

	/// <summary>
	/// Table LocalAreasFacilityPlaces
	/// </summary>
	public virtual ICollection<FacilityPlace> AvailableFacilityPlaces { get; set; }
	[InverseProperty(nameof(FacilityPlace.LocalArea))]
	public virtual ICollection<FacilityPlace> FacilityPlaces { get; set; }
	public virtual ICollection<PersonWorkArea> PersonWorkAreas { get; set; }

	public virtual ICollection<CeremonyOrder> CeremonyOrders { get; set; }

	/// <summary>
	/// Table LocalAreaSignContractPeople
	/// </summary>
	public virtual ICollection<Person> AccessToSignContracts { get; set; }
}