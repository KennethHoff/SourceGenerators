using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CountySettings
{
	public required Guid CeremonyTypeId { get; set; }
	public required Guid CountyId { get; set; }
	public required Guid SeasonId { get; set; }
	[Column(TypeName = "money")]
	public required decimal Price { get; set; }
	[Column(TypeName = "money")]
	public required decimal? MemberPrice { get; set; }
	public required string Description { get; set; }
	public required bool Visible { get; set; }
	public required bool HiddenWeb { get; set; }

	[StringLength(300)]
	public required string EmailMerge { get; set; }
	[StringLength(50)]
	public required string PhoneContact { get; set; }
	[StringLength(300)]
	public required string WebPageUrl { get; set; }
        
	public required int? PaymentDeadline { get; set; }
	public required int? PaymentReminder { get; set; }
	public required bool CombineLocalAreas { get; set; }
	public required DateTime? SaleStart { get; set; }
	public required DateTime? SaleStartMembers { get; set; }

	public required bool IsSeasonStatisticGenerated { get; set; }

	[ForeignKey("CountyId")]
	public virtual required County County { get; set; }
	[ForeignKey("SeasonId")]
	public virtual required Season Season { get; set; }
}