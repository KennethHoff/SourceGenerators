using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class County
{

	[Required]
	[StringLength(500)]
	public required string Name { get; set; }
	[StringLength(500)]
	public required string ShortName { get; set; }
	public required int? PrisolveId { get; set; }



	public virtual required ICollection<Ceremony> Ceremonies { get; set; }
	public virtual required ICollection<CountySettings> CountySettings { get; set; }
	public virtual required ICollection<School> Schools { get; set; }
	public virtual required ICollection<LocalArea> LocalAreas { get; set; }
	public virtual required ICollection<Person> PreferredByPersons { get; set; }
	public virtual required ICollection<Person> AccessibleByPersons { get; set; }
	public virtual required ICollection<PersonWorkArea> PersonWorkAreas { get; set; }
	public virtual required ICollection<Activity> Activites { get; set; }
	public virtual required ICollection<CeremonyOrder> CeremonyOrders { get; set; }
	public virtual required ICollection<Partner> Partners { get; set; }
}