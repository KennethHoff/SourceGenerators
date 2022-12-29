namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class Season
{
    public DateTime? ConfirmationEarlyRefundDeadline { get; set; }
    public DateTime? ConfirmationExportPaymentStartDate { get; set; }
    public int ConfirmationNewOrdersRefundDeadlineDays { get; set; }
    public decimal? ConfirmationCancellationFee { get; set; }
    public decimal? ConfirmationCourseCancellationAdministrationFee { get; set; }
    public decimal? ConfirmationCourseChangeAdministrationFee { get; set; }
    public decimal? ConfirmationCeremonyCancellationRefundAmount { get; set; }
    public bool ActivateCourseLeaderReminderNotification { get; set; }
    public int ConfirmationHumanStartedOrderExpirationHours { get; set; }

    public int ConfirmationReplyToWaitingListOfferHours { get; set; }
    public int ConfirmationReminderToReplyToWaitingListOfferHoursBeforeExpiration { get; set; }

    public string? ConfirmandConsentUrl { get; set; }
    public int Year { get; set; }
    public DateTime? ConfirmationMemberSaleStart { get; set; }
    public DateTime? ConfirmationDisableChangeCourseAfterDate { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
    public virtual ICollection<Ceremony> Ceremonies { get; set; } = new List<Ceremony>();
    public virtual ICollection<CountySettings> CountySettings { get; set; } = new List<CountySettings>();
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<LocalAreaSettings> LocalAreaSettings { get; set; } = new List<LocalAreaSettings>();
    public virtual ICollection<CeremonyOrder> CeremonyOrders { get; set; } = new List<CeremonyOrder>();
    public virtual ICollection<PersonWorkArea> PersonWorkAreas { get; set; } = new List<PersonWorkArea>();


    #region SKT (Studieforbundet: Kultur og Tradisjon)

    public string? SKTApplicationRoundCode { get; set; }

    public string? SKTCoursePlanCode { get; set; }

    /// <summary> Earliest time for submissions of SKT Applications </summary>
    public DateTime? SKTApplicationSubmissionStartDate { get; set; }

    #endregion
}
