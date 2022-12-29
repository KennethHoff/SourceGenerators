using System.ComponentModel.DataAnnotations;

namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class MessageTemplate
{
	public required string Body { get; set; }
	[StringLength(512)]
	public required string Subject { get; set; }
}