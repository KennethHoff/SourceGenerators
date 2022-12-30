namespace AnotherProject.Seremonibasen.Models.WebProject.Models;

[SchemaObject]
public class CeremonyContractTemplate
{
    public string? Title { get; set; }
    public string? SubTitleLine1 { get; set; }
    public string? SubTitleLine2 { get; set; }
    public string? SubTitleLine3 { get; set; }
    public string? SubTitleLine4 { get; set; }
    
    public string? Text1Title { get; set; }
    public string? Text1 { get; set; }
    public string? Text2Title { get; set; }
    public string? Text2 { get; set; }
    
    public string? CeremonyTitle { get; set; }
    public string? CeremonyDateLabel { get; set; }
    public string? CeremonyPlaceLabel { get; set; }
    
    public string? SignatureTitle { get; set; }
    public string? SignaturePlaceLabel { get; set; }
    public string? SignatureDateLabel { get; set; }
    public string? SignatureIdControlLabel { get; set; }
    public string? SignatureHEF { get; set; }
}
