using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class AssignmentRequest
{
	/// <summary>
	/// Date request is sent
	/// </summary>
	public DateTime? RequestSentDate { get; set; }

	/// <summary>
	/// Date person responded
	/// </summary>
	public DateTime? RequestResponseDate { get; set; }


	/// <summary>
	/// Set to true if assignment is assiged to leader
	/// </summary>
	public bool IsExpired { get; set; }

	/// <summary>
	/// Order request is for
	/// </summary>
	public Guid CeremonyOrderId { get; set; }
	[ForeignKey("CeremonyOrderId")]
	public virtual required CeremonyOrder CeremonyOrder { get; set; }


	/// <summary>
	/// Person request is sent to
	/// </summary>
	public Guid PersonId { get; set; }
	[ForeignKey("PersonId")]
	public virtual required Person Person { get; set; }
}