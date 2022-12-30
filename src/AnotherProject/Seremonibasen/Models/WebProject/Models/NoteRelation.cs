using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class NoteRelation
{
	public required Guid RelatedToId { get; set; }

	[StringLength(512)]
	public required string CreatedByName { get; set; }
	public required string Text { get; set; } //anonymiser s-1m

	//slett hele ved 31des-6m

	public required Guid? FollowedUpById { get; set; }
	public required DateTime? FollowedUpDate { get; set; }

}