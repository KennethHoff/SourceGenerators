using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class FileRelation
{
	public Guid RelatedToId { get; set; }
	[Required]
	[StringLength(512)]
	public required string FileName { get; set; }

	/// <summary>
	/// epiFileId
	/// </summary>
	[Required]
	[StringLength(512)]
	public required string FilePath { get; set; }
	[Required]
	[StringLength(5)]
	public required string FileExtension { get; set; }
	public required long FileSize { get; set; }
	public required string Description { get; set; }
}