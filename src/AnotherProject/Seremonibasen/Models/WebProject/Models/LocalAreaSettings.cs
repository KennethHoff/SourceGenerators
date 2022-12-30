using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class LocalAreaSettings
{
	public required Guid CeremonyTypeId { get; set; }
	public required Guid LocalAreaId { get; set; }
	public required string Description { get; set; }
	public required Guid SeasonId { get; set; }
	public required bool ShowNameInNewspaper { get; set; }
	public required bool ShowApproveGroupImages { get; set; }
	public required bool ShowApproveInternalImages { get; set; }
	public required bool ShowApprovePublicSocialMediaImages { get; set; }
	public required bool ShowApprovePublicHEFImages { get; set; }
	public required bool ShowApproveStreaming { get; set; }
	public required bool IsMerkedagActive { get; set; }
	public required bool ShowNrOfGuestSeats { get; set; }
	public required bool ShowSchools { get; set; }
	public required bool ShowContribution { get; set; }
	[StringLength(300)]
	public required string Email { get; set; }
	public required bool AcceptsOtherLocalAreas { get; set; }
	public required bool Visible { get; set; }
	public required bool HiddenWeb { get; set; }
	public required int? GenderBalance { get; set; }
	public required int? MaxNumberOfConfirmandsOnWaitingList { get; set; }

	public required string ValidZipCodes { get; set; }

	[ForeignKey("LocalAreaId")]
	public virtual required LocalArea LocalArea { get; set; }
	[ForeignKey("SeasonId")]
	public virtual required Season Season { get; set; }

	public required string GuestSeatsInformation { get; set; }
}