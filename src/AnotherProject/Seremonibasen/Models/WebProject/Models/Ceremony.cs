using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Ceremony
{
        
	public required DateTime? CertificateGeneratedDateTime { get; set; }
	public required Guid SeasonId { get; set; }
	public required Guid CeremonyTypeId { get; set; }
	public required DateTime DateAndTimeFrom { get; set; }

	public required bool IsMerkedagActive { get; set; }

	/// <summary>
	/// Used in NameParty Ceremony
	/// </summary>
	public required int NumberOfSeats { get; set; }
	public required int NumberOfSeatsReservedByHEF { get; set; }
	public required int NumberOfSeatsReserved { get; set; }
	public required bool ConfirmandsOnStage { get; set; }
	[Required]
	public required int CeremonyNumber { get; set; }
	public required bool Visible { get; set; }
	public required int TripletexId { get; set; }
	public required Guid? CountyId { get; set; }
	public required Guid? FacilityRoomId { get; set; }
	public required Guid? CeremonyLeaderId { get; set; }
	public required Guid? LocalAreaId { get; set; }
	public required int Weekday { get; set; }

	public required bool IsCapacityStatisticGenerated { get; set; }

	public required DateTime? SurveyGeneratedDateTime { get; set; }

	public required bool IsCancelled { get; set; }
	public required DateTime? CancelledDateTime { get; set; }
	public required decimal? CancelledRefundAmount { get; set; }
	public required string StatusText { get; set; }

	public required DateTime? ServiceBusLastSentDate { get; set; }
	public required DateTime? ServiceBusLastReceivedDate { get; set; }
	public required string ServiceBusLastReceivedOrigin { get; set; }

	[ForeignKey("LocalAreaId")]
	public virtual required LocalArea? LocalArea { get; set; }
	/// <summary>
	/// Table LocalAreaCeremonies
	/// </summary>
	public virtual required ICollection<LocalArea>? ForSaleInLocalAreas { get; set; }

	public required Guid? CeremonyFormId { get; set; }
	[ForeignKey("CeremonyFormId")]
	public virtual required CeremonyForm? CeremonyForm { get; set; }

	[ForeignKey("CountyId")]
	public virtual required County? County { get; set; }
	[ForeignKey("FacilityRoomId")]
	public virtual required FacilityRoom? FacilityRoom { get; set; }
	[ForeignKey("SeasonId")]
	public virtual required Season? Season { get; set; }
	[ForeignKey("CeremonyLeaderId")]
	public virtual required Person? CeremonyLeader { get; set; }
	public virtual required ICollection<Person>? Assistants { get; set; }

	[ForeignKey(nameof(CeremonyOrder.CeremonyId))]
	public virtual required ICollection<CeremonyOrder>? Orders { get; set; }

	[ForeignKey(nameof(CeremonyOrder.WaitingListCeremonyId))]
	public virtual required ICollection<CeremonyOrder>? WaitingListCeremonyOrders { get; set; }
	public virtual required ICollection<Course>? Courses { get; set; }
	public virtual required ICollection<MessageLog>? MessageLogs { get; set; }
	public virtual required ICollection<Remuneration>? Remunerations { get; set; }
	public virtual required ICollection<CeremonyLeaderOption>? CeremonyLeaderOptions { get; set; }


	public required Guid? OriginalId { get; set; }
}