namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

/// <summary>
/// Employee Contract template
/// </summary>
[SchemaObject]
public class ContractTemplate
{
	public required string Title { get; set; }
	public required string SubTitle { get; set; }
	public required string Body { get; set; }
	public required string Confidentiality { get; set; }
}