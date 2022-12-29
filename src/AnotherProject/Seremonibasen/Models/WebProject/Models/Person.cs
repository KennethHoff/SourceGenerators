using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
[Table("People")]
public class Person
{

	/// <summary>
	/// Membership number from CRM  max length 50
	/// </summary>
	[StringLength(50)]
	public required string MembershipNumber { get; set; }

	[StringLength(50)]
	public required string BankAccountNumber { get; set; }

	/// <summary>
	/// (Gravferd: Tilknytning til avdøde)
	/// </summary>
	public required string Comment { get; set; }

	/// <summary>
	/// Avsjekk/samtykke til at man godtar at honorarer rett over 10.000 kan justeres ned
	/// </summary>
	public required bool? ApprovedReducedPayoutForTaxBenefits { get; set; }


	/// <summary>
	/// Mysoft export status
	/// </summary>

	/// <summary>
	/// Error message from export
	/// </summary>
	public required string MySoftExportErrroMessage { get; set; }


	/// <summary>
	/// Current membership status
	/// </summary>

	/// <summary>
	/// Username (max length 300)
	/// </summary>
	[StringLength(300)]
	public required string Username { get; set; }

	/// <summary>
	/// First name (max length 500)
	/// </summary>
	[StringLength(500)]
	public required string FirstName { get; set; }

	/// <summary>
	/// Surname (max length 500)
	/// </summary>
	[StringLength(500)]
	public required string Surname { get; set; }

	/// <summary>
	/// Is person employee at HEF
	/// </summary>
	public required bool IsEmployee { get; set; }

	/// <summary>
	/// Is the person logging in with ADFS
	/// </summary>
	public required bool IsFederationUser { get; set; }

	/// <summary>
	/// Email address (max length 300)
	/// </summary>
	[StringLength(300)]
	public required string Email { get; set; }

	public required bool IsCreatedManually { get; set; }

	/// <summary>
	/// SSN number. (max length 20). only numeric values saved to database
	/// </summary>
	[StringLength(20)]
	public required string? SSN { get; set; }

	/// <summary>
	/// A guid unique per SSN in the database
	/// </summary>
	public required Guid? SsnGuid { get; set; }

	/// <summary>
	/// Mobile number. (max length 20). only numeric values saved to database
	/// </summary>
	[StringLength(20)]
	public required string Mobile { get; set; }

	/// <summary>
	/// Gender
	/// </summary>

	/// <summary>
	/// Birth date
	/// </summary>
	public required DateTime? BirthDate { get; set; }

	/// <summary>
	/// Death date
	/// </summary>
	public required DateTime? DeathDate { get; set; }
       
	/// <summary>
	/// StreetAddress (max length 300)
	/// </summary>
	[StringLength(300)]
	public required string StreetAddress { get; set; }

	/// <summary>
	/// Post code (max length 10)
	/// </summary>
	[StringLength(10)]
	public required string? PostCode { get; set; }

	/// <summary>
	/// City (max length 20)
	/// </summary>
	[StringLength(300)]
	public required string? City { get; set; }

	/// <summary>
	/// Is active person. currently used for user
	/// </summary>
	public required bool IsActive { get; set; }

	/// <summary>
	/// EmployeeCountyId
	/// </summary>
	public required Guid? EmployeeCountyId { get; set; }

	/// <summary>
	/// EmployeeLocalAreaId
	/// </summary>
	public required Guid? EmployeeLocalAreaId { get; set; }

	/// <summary>
	/// Tripletex Id
	/// </summary>
	public required int TripletexId { get; set; }

	/// <summary>
	/// Employee number tripletex
	/// </summary>
	[StringLength(50)]
	public required string EmployeeNumber { get; set; }

	/// <summary>
	/// Manually control for it employee is exported to tripletex.
	/// Controls is person is included in CSV export file
	/// </summary>
	public required bool IsExportedToTripletex { get; set; }


	/// <summary>
	/// Customer number tripletex
	/// </summary>
	[StringLength(50)]
	public required string CustomerNumber { get; set; }

	/// <summary>
	/// Parent volenteer to contribute in confirmation (Confirmation)
	/// </summary>
	public required bool? WantToContributeForCeremony { get; set; }
	/// <summary>
	/// Used for 2-factor verification
	/// </summary>     
	public required byte[] VerificationHash { get; set; }
	/// <summary>
	/// Used for 2-factor verification
	/// </summary>
	public required byte[] VerificationSalt { get; set; }
	/// <summary>
	/// Used for 2-factor verification
	/// </summary>
	public required DateTime? SMSVerificationCodeExpiresDate { get; set; }


	/// <summary>
	/// The person's identity is verified by Bank ID
	/// </summary>
	public required bool? BankIdVerified { get; set; }
	/// <summary>
	/// Last date for verification request.
	/// </summary>
	public required DateTime? LastBankIdVerificationRequestDate { get; set; }


	public required Guid? MysoftPersonId { get; set; }

	public required DateTime? ServiceBusLastSentDate { get; set; }
	public required DateTime? ServiceBusLastReceivedDate { get; set; }
	public required string ServiceBusLastReceivedOrigin { get; set; }


	[ForeignKey("EmployeeCountyId")]
	public virtual required County EmployeeCounty { get; set; }
	[ForeignKey("EmployeeLocalAreaId")]
	public virtual required LocalArea EmployeeLocalArea { get; set; }
	public virtual required ICollection<CeremonyOrderPerson> CeremonyOrderPersons { get; set; }
	public virtual required ICollection<ChangeLog> ChangeLogs { get; set; }
	public virtual required ICollection<CourseDayParticipation> CourseDayParticipations { get; set; }
	public virtual required ICollection<County> PreferredCounties { get; set; }
	public virtual required ICollection<County> AccessToCounties { get; set; }
	public virtual required ICollection<LocalArea> AccessToLocalArea { get; set; }
	public virtual required ICollection<Role> Roles { get; set; }
	public virtual required ICollection<PersonWorkArea> WorkAreas { get; set; }
	/// <summary>
	/// Preferred ceremony types
	/// </summary>
	[InverseProperty(nameof(Event.Owner))]
	public virtual required ICollection<Event> Events { get; set; }

	public virtual required ICollection<Course> AssistantForCourses { get; set; }
	public virtual required ICollection<Course> SubstituteForCourses { get; set; }
	public virtual required ICollection<Course> CourseLeaderFor { get; set; }
	public virtual required ICollection<Ceremony> CeremonyLeaderFor { get; set; }
	public virtual required ICollection<Ceremony> AssistantForCeremonies { get; set; }

	public virtual required ICollection<LocalArea> SignContractsInLocalArea { get; set; }

	public virtual required ICollection<Contract> Contracts { get; set; }
	public virtual required ICollection<Contract> SignedContractsAsHEF { get; set; }
	public virtual required ICollection<Contract> SignedContracts { get; set; }

	public virtual required ICollection<Remuneration> Remunerations { get; set; }
	public virtual required ICollection<Remuneration> ApprovedRemunerations { get; set; }
	public virtual required ICollection<Remuneration> ExportedRemunerations { get; set; }

	[ForeignKey("PersonId")]
	public virtual required ICollection<AssignmentRequest> AssignmentRequests { get; set; }

	[ForeignKey("PersonId")]
	public virtual required ICollection<CeremonyLeaderOption> CeremonyLeaderOptions { get; set; }

	[ForeignKey(nameof(CeremonyOrderMarriageImport.ApprovedByEmployeeId))]
	public virtual required ICollection<CeremonyOrderMarriageImport> CeremonyOrderMarriageImports { get; set; }

}