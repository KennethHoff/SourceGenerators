using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyOrder
{
    public required string PreferredCeremonyLocation { get; set; }
    public required Guid? CompletedSignedByUserId { get; set; }
    public required DateTime? CompletedSignedDate { get; set; }
    public required Guid? CompletedRemunerationsByUserId { get; set; }
    public required DateTime? CompletedRemunerationsDate { get; set; }

    public required DateTime? LastLeaderNotificationDate { get; set; }

    [StringLength(50)]
    public required string CustomerBankAccountNumber { get; set; }
    public required bool? IsDeactivatedFromApi { get; set; }
    public required decimal? CancelledPaymentAmount { get; set; }
    public required DateTime? CancelledPaymentAmountGeneratedDateTime { get; set; }
    public required decimal? CeremonyRefundAmount { get; set; }
    public required bool? IsCancelledFromApi { get; set; }
    public required bool? IsCancelled { get; set; }
    public required bool? IsPriceChange { get; set; }
    public required DateTime? CeremonyOrderReservationExpirationDateTime { get; set; }


    /// <summary>
    /// Confirmed membership
    /// </summary>
    public required bool ConfirmedMembership { get; set; }

    /// <summary>
    /// Confirmands School (Confirmation)
    /// </summary>
    public required Guid? SchoolId { get; set; }

    /// <summary>
    /// Other School, should not be used after the order is created, we create new schools and set id
    /// </summary>
    [StringLength(500)]
    public required string? OtherSchool { get; set; }

    /// <summary>
    /// Do secondary contact also require all information sent
    /// </summary>
    public required bool SendInformationToSecondaryContactPerson { get; set; }


    /// <summary>
    /// Confirmands gender. Used for seat distribution (Confirmation)
    /// </summary>

    /// <summary>
    /// Granted number of guest seats for ceremony (Confirmation and NameParty)
    /// </summary>
    public required int GrantedNrOfGuestSeats { get; set; }

    /// <summary>
    /// Sent granted number of guest seats for ceremony (Confirmation and NameParty)
    /// </summary>
    public required int? SentGrantedNrOfGuestSeats { get; set; }

    /// <summary>
    /// Number of guest seats used (Merkedag)
    /// </summary>
    public required int? NumberOfGuests { get; set; }

    /// <summary>
    /// Number of guest seats checked in (Merkedag)
    /// </summary>
    public required int? NumberOfGuestsCheckedIn { get; set; }

    /// <summary>
    /// Sent granted number of guest seats for ceremony date (Confirmation and NameParty)
    /// </summary>
    public required DateTime? SentGrantedNrOfGuestSeatsDate { get; set; }

    /// <summary>
    /// Granted number of guest seats from human.no form (Confirmation and NameParty)
    /// </summary>
    public required int PreferredNrOfGuestSeats { get; set; }

    /// <summary>
    /// Number og seats not granted for ceremony (Confirmation and NameParty)
    /// </summary>
    public required int WaitingListNrOfGuestSeats { get; set; }

    /// <summary>
    /// Default false, som localareas ask for this in human.no form (Confirmation and NameParty)
    /// </summary>
    public required bool? ApprovePublishToLocalNewspaper { get; set; }

    /// <summary>
    /// Er det greit at det tas bilder av konfirmanten til intern bruk f.eks. i kurs/leir/seremoni?
    /// </summary>

    /// <summary>
    /// Er det greit at det tas gruppebilde av konfirmanten f.eks. på kurs/seremoni
    /// </summary>

    /// <summary>
    /// Det er greit at det tas gruppebilde av konfirmanten på kurs/seremoni
    /// </summary>

    /// <summary>
    /// Det er greit at det tas bilder av konfirmanten til bruk i gruppen, f. eks. på kurs/leir/seremoni
    /// </summary>

    /// <summary>
    /// Det er greit at bilder/film av konfirmanten publiseres på våre nettsider eller i sosiale medier (f. eks. Facebook og Instagram) i forbindelse med profilering av Humanistisk konfirmasjon hos Human-Etisk Forbund. 
    /// </summary>

    /// <summary>
    /// Det er greit at bilder/film av konfirmanten publiseres i egenproduserte trykksaker om Humanistisk konfirmasjon hos Human-Etisk Forbund. 
    /// </summary>
    /// <summary>
    /// Konfirmants samtykke til foreldres bildesamtykker
    /// </summary>

    /// <summary>
    /// Det er greit at konfirmanten blir filmet i forbindelse av strømming av seremonien
    /// </summary>

    /// <summary>
    /// Konfirmants samtykke til foreldres bildesamtykker dato satt
    /// </summary>
    public required DateTime? ConsentConfirmandDateTime { get; set; }


    public required string ConsentUrlCode { get; set; }
    public required DateTime? ConsentUrlCodeGeneratedDateTime { get; set; }

    public required string OtherContactVerificationUrlCode { get; set; }
    public required DateTime? OtherContactVerificationCodeGeneratedDateTime { get; set; }

    /// <summary>
    /// Comment set from human.no form. (Confirmation, NameParty and Wedding)
    /// </summary>
    public required string Comment { get; set; }


    /// <summary>
    /// Whenever user updates the reservation from human.no form
    /// </summary>
    public required DateTime? ReservationUpdatedDate { get; set; }
    /// <summary>
    /// Status flags for reminders for user to complete the reservation on human.no.
    /// </summary>

    /// <summary>
    /// Status for order. Controls if and where it is picked up by order processing jobs (Confirmation, NameParty)
    /// </summary>


    
    public required DateTime? MarriageRegisterStatusChangedDate { get; set; }
    public required Guid? MarriageRegisterStatusChangedById { get; set; }
   
    [ForeignKey(nameof(MarriageRegisterStatusChangedById))]
    public virtual required Person? MarriageRegisterStatusChangedBy { get; set; }
    
    public required DateTime? SpeakerConfirmedDate { get; set; }

    /// <summary>
    /// Confirmation for order is sent status flag (Confirmation, NameParty, Wedding, funeral)
    /// </summary>

    /// <summary>
    /// Total amount for order discounts and extras. (Confirmation, NameParty, Wedding, Funeral) 
    /// </summary>
    [Column(TypeName = "money")]
    public required decimal? PaymentAmount { get; set; }


    /// <summary>
    /// Total custom amount for order. PaymentAmount should be the same (Confirmation, NameParty, Wedding, Funeral) 
    /// </summary>
    [Column(TypeName = "money")]
    public required decimal? PaymentAmountCustom { get; set; }
        
        
    /// <summary>
    /// Total amount for order from tripletex (Confirmation, NameParty, Wedding, Funeral) 
    /// </summary>
    [Column(TypeName = "money")]
    public required decimal? TripletexPaymentAmount { get; set; }

    /// <summary>
    /// Space separated list of Invoice numbers (Confirmation, NameParty, Wedding, Funeral) 
    /// </summary>
    public required string InvoiceNumbers { get; set; }

    /// <summary>
    /// </summary>
    [Column(TypeName = "money")]
    public required decimal? PaidAmount { get; set; }

    /// <summary>
    /// Reg number Unique Database generated option
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int OrderNumber { get; set; }

    /// <summary>
    /// Invoice due date
    /// </summary>
    public required DateTime? InvoiceDueDate { get; set; }

    /// <summary>
    /// Tripletex Id saving to tripletex should return id
    /// </summary>
    public required int TripletexId { get; set; }

    /// <summary>
    /// Ceremony type Id
    /// </summary>
    public required Guid CeremonyTypeId { get; set; }

    /// <summary>
    /// A unique ID per SSN for the person who made the order
    /// <seealso cref="Person.SsnGuid"/>
    /// </summary>

    /// <summary>
    /// Ceremony id (Confirmation, NameParty)
    /// </summary>
    public required Guid? CeremonyId { get; set; }

    /// <summary>
    /// Waiting list ceremony (NameParty)
    /// </summary>
    public required Guid? WaitingListCeremonyId { get; set; }

    /// <summary>
    /// Course id (Confirmation)
    /// </summary>
    public required Guid? CourseId { get; set; }

    /// <summary>
    /// Waiting list course id (Confirmation)
    /// </summary>
    public required Guid? WaitingListCourseId { get; set; }

    public required DateTime? WaitingListSetDate { get; set; }
    public required Guid? WaitingListOfferedByUserId { get; set; }
    public required DateTime? WaitingListOfferedDateTime { get; set; }
    public required Guid? WaitingListResponseUserId { get; set; }
    public required DateTime? WaitingListResponseDateTime { get; set; }
    public required DateTime? WaitingListAssignedCommunicationSentDateTime { get; set; }
    public required DateTime? WaitingListAssignedDateTime { get; set; }
    public required bool WaitingListOfferExpiresSoonReminderSent { get; set; }

    public required DateTime? ServiceBusLastSentDate { get; set; }
    public required DateTime? ServiceBusLastReceivedDate { get; set; }
    public required string ServiceBusLastReceivedOrigin { get; set; }

    /// <summary>
    /// Course id (Confirmation)
    /// </summary>
    public required Guid? PreviousCourseId { get; set; }

    public required DateTime? ChangeCourseDate { get; set; }
    public required bool? ChangeCourseNewInvoice { get; set; }

    /// <summary>
    /// Needs to be kept in sync with course.SeasonId for Confirmation <br/>
    /// Needs to be kept in sync with ceremony.SeasonId for NameParty
    /// </summary>
    public required Guid? SeasonId { get; set; }

    /// <summary>
    /// Needs to in sync with Course.LocalArea.CountyId if confirmation.
    /// Needs to in sync with Ceremony.LocalArea.CountyId if name party.
    /// (Confirmation, Name party, Wedding, Funeral??)
    /// </summary>
    public required Guid? CountyId { get; set; }

    /// <summary>
    /// Needs to in sync with Course.LocalAreaId for confirmation<br/>
    /// Needs to in sync with Ceremony.LocalAreaId for NameParty<br/>
    /// Should be null for all other ceremony types.
    /// </summary>
    public required Guid? LocalAreaId { get; set; }

    /// <summary>
    /// Date for "kursattest" email
    /// </summary>
    public required DateTime? CourseCompletedEmailDate { get; set; }


    public required bool IsOrderStatisticGenerated { get; set; }


    /// <summary>
    /// HG and HV
    /// </summary>
    public required Guid? CeremonyOrderFormId { get; set; }



    //RELATIONS

    [ForeignKey(nameof(CeremonyOrderFormId))]
    public virtual required CeremonyOrderForm? CeremonyOrderForm { get; set; }


    [ForeignKey(nameof(SeasonId))]
    public virtual required Season? Season { get; set; }

    [ForeignKey(nameof(CountyId))]
    public virtual required County? County { get; set; }

    [ForeignKey(nameof(LocalAreaId))]
    public virtual required LocalArea? LocalArea { get; set; }


    [ForeignKey(nameof(WaitingListCeremonyId))]
    public virtual required Ceremony? WaitingListCeremony { get; set; }


    [ForeignKey(nameof(CourseId))]
    public virtual required Course? Course { get; set; }

    [ForeignKey(nameof(WaitingListCourseId))]
    public virtual required Course? WaitingListCourse { get; set; }

    [ForeignKey(nameof(PreviousCourseId))]
    public virtual required Course? PreviousCourse { get; set; }



    /// <summary>
    /// Relation to people connected to order with order role
    /// </summary>
    public virtual required ICollection<CeremonyOrderPerson> CeremonyOrderPersons { get; set; }

    [ForeignKey("RelatedToId")]
    public virtual required ICollection<NoteRelation> NoteRelations { get; set; }

    public virtual required ICollection<MessageLog> MessageLogs { get; set; }

    [ForeignKey("CeremonyOrderId")]
    public virtual required ICollection<AssignmentRequest> AssignmentRequests { get; set; }

    [ForeignKey("CeremonyOrderId")]
    public virtual required ICollection<CeremonyOrderSibling> Siblings { get; set; }

    [ForeignKey("SiblingCeremonyOrderId")]
    public virtual required ICollection<CeremonyOrderSibling> ConnectedSiblings { get; set; }

    public required Guid? PartnerId { get; set; }
    [ForeignKey(nameof(PartnerId))]
    public virtual required Partner? Partner { get; set; }
    
    public required Guid? PartnerContactId { get; set; }
    [ForeignKey(nameof(PartnerContactId))]
    public virtual required PartnerContact? PartnerContact { get; set; }
}
