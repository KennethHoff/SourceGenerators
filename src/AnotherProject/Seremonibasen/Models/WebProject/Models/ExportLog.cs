namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class ExportLog
{
        
	public required string SearchId { get; set; }

	public required Guid? CeremonyId { get; set; }
        
	public required Guid? CourseId { get; set; }
        
	public required Guid? ActivityId { get; set; }
	public required Guid? CountyId { get; set; }
	public required Guid? LocalAreaId { get; set; }
	public required Guid? SeasonId { get; set; }

       

	public required DateTime? CheckedOutDate { get; set; }
	public required DateTime? CheckedInDate { get; set; }
	public required bool IsConfidential { get; set; }


}