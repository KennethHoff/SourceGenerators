using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Course
{
	public required Guid? OriginalId { get; set; }
	public required Guid? CompletedSignedByUserId { get; set; }
	public required DateTime? CompletedSignedDate { get; set; }
	public required Guid? CompletedRemunerationsByUserId { get; set; }
	public required DateTime? CompletedRemunerationsDate { get; set; }
	public required DateTime? CertificateGeneratedDateTime { get; set; }
	public required Guid SeasonId { get; set; }

	[Required]
	[StringLength(500)]
	public required string Name { get; set; }

	[Required]
	public required int CourseNumber { get; set; }

	public required DateTime DateAndTimeFrom { get; set; }
	public required int NumberOfSeats { get; set; }
	public required bool Visible { get; set; }
	public required int? GenderBalance { get; set; }
	public required int TripletexId { get; set; }
	public required int Weekday { get; set; }

	public required DateTime? LastCourseLeaderNotificationDate { get; set; }

	public required bool IsCapacityStatisticGenerated { get; set; }

	public required Guid? CourseTypeId { get; set; }
	public required Guid? FacilityRoomId { get; set; }
	public required Guid? CourseLeaderId { get; set; }
	public required Guid? CeremonyId { get; set; }
	public required Guid? LocalAreaId { get; set; }

	public required bool IsCancelled { get; set; }
	public required DateTime? CancelledActivitiesDateTime { get; set; }
	public required string StatusText { get; set; }
	public required DateTime? CancelledDateTime { get; set; }
	public required decimal? AdministrationFee { get; set; }

	public required DateTime? ServiceBusLastSentDate { get; set; }
	public required DateTime? ServiceBusLastReceivedDate { get; set; }
	public required string ServiceBusLastReceivedOrigin { get; set; }

	[ForeignKey("LocalAreaId")]
	public virtual required LocalArea LocalArea { get; set; }

	[ForeignKey("CeremonyId")]
	public virtual required Ceremony Ceremony { get; set; }

	[ForeignKey("CourseTypeId")]
	public virtual required CourseType CourseType { get; set; }

	[ForeignKey("FacilityRoomId")]
	public virtual required FacilityRoom FacilityRoom { get; set; }

	[ForeignKey("SeasonId")]
	public virtual required Season Season { get; set; }

	[ForeignKey("CourseLeaderId")]
	public virtual required Person CourseLeader { get; set; }

	public virtual required ICollection<Person> Assistants { get; set; }

	public virtual required ICollection<Person> Substitutes { get; set; }


	[ForeignKey("CourseId")]
	public virtual required ICollection<CeremonyOrder> CeremonyOrders { get; set; }

	[ForeignKey("WaitingListCourseId")]
	public virtual required ICollection<CeremonyOrder> WaitingListCeremonyOrders { get; set; }

	[ForeignKey("PreviousCourseId")]
	public virtual required ICollection<CeremonyOrder> PreviousCeremonyOrders { get; set; }

	public virtual required ICollection<CourseDay> CourseDays { get; set; }
	public virtual required ICollection<CourseReservation> CourseReservations { get; set; }
	public virtual required ICollection<Activity> Activities { get; set; }
	public virtual required ICollection<Activity> CancelledActivities { get; set; }

	public virtual required ICollection<MessageLog> MessageLogs { get; set; }

	/// <summary>
	/// Table ForSaleCoursesInLocalArea
	/// </summary>
	public virtual required ICollection<LocalArea> ForSaleInLocalAreas { get; set; }

	public virtual required ICollection<Remuneration> Remunerations { get; set; }
}