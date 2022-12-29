using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Contract
{

	public required string Title { get; set; }
	public required string SubTitle { get; set; }
	public required string Body { get; set; }
	public required string Confidentiality { get; set; }

	public required Guid? PersonId { get; set; }
	[ForeignKey("PersonId")]
	public required Person Person { get; set; }

	public required Guid? SeasonId { get; set; }
	[ForeignKey("SeasonId")]
	public required Season Season { get; set; }

	public required Guid? CountyId { get; set; }
	[ForeignKey("CountyId")]
	public required County County { get; set; }

	public required Guid? LocalAreaId { get; set; }
	[ForeignKey("LocalAreaId")]
	public required LocalArea LocalArea { get; set; }

	public required Guid? DraftEditUserId { get; set; }
	public required DateTime? DraftEditDateTime { get; set; }
	public required Guid? SignedByHEFUserId { get; set; }
	[ForeignKey("SignedByHEFUserId")]
	public required Person SignedByHEF { get; set; }
	public required DateTime? SignedHEFDate { get; set; }
	public required Guid? SignedByWorkerUserId { get; set; }
	[ForeignKey("SignedByWorkerUserId")]
	public required Person SignedByWorker { get; set; }
	public required DateTime? SignedWorkerDate { get; set; }

	public virtual required ICollection<ContractRate> ContractRates { get; set; }
}