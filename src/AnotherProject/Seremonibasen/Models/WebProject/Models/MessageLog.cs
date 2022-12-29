using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class MessageLog
{
	[Required]
	public required string Receiver { get; set; }
	public required string Text { get; set; }
	public required bool? ValidRelatedToId {get; set;}
	public required Guid RelatedToId { get; set; }
	public required Guid? ReceiverId { get; set; }
       
	public required Guid? ReceiverGroupId { get; set; }
	public required string ErrorMessage { get; set; }
	public required bool IsSentOnServiceBus { get; set; }


	[StringLength(300)]
	public required string Mobile { get; set; }

	public required string Name { get; set; }

	public required string From { get; set; }


	public required string FromJson { get; set; }
	public required string ToJson { get; set; }
	public required string CcJson { get; set; }
	public required string BccJson { get; set; }
	public required string ReplyToJson { get; set; }
	public required string CategoriesJson { get; set; }
	public required string Subject { get; set; }
	public required string AttachmentsFileRelationIdJson { get; set; }
	public required bool IsHtml { get; set; }
	public required bool SendSMSNotification { get; set; }


	public required DateTime? SentDate { get; set; }
	public required Guid? SentById { get; set; }
	[ForeignKey(nameof(SentById))]
	public virtual required Person SentBy { get; set; }

	public required Guid? CeremonyOrderId { get; set; }
	[ForeignKey("CeremonyOrderId")]
	public required CeremonyOrder CeremonyOrder { get; set; }

	public required Guid? CourseId { get; set; }
	[ForeignKey("CourseId")]
	public required Course Course { get; set; }

	public required Guid? CeremonyId { get; set; }
	[ForeignKey("CeremonyId")]
	public required Ceremony Ceremony { get; set; }


}