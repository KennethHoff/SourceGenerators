using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Event
{
	public required DateTime Start { get; set; }
	public required DateTime End { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public required Guid OwnerId { get; set; }
	[ForeignKey("OwnerId")]
	public virtual required Person Owner { get; set; }


}