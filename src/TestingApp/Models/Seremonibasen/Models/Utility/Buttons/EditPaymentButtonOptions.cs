
namespace TestingApp.Models.Seremonibasen.Models.Utility.Buttons;

public struct EditPaymentButtonOptions
{
    public EditPaymentButtonOptions()
    { }

    public bool UseExportButton { get; set; } = false;
    public bool UseProcessingButton { get; set; } = false;
    public bool UseResendEmailButton { get; set; } = false;
    public bool UseUpdateInvoiceButton { get; set; } = false;

    public required string ExportButtonText { get; set; }
    public required string ProcessingButtonText { get; set; }
    public required string ResendEmailButtonText { get; set; }
    public required string UpdateInvoiceButtonText { get; set; }
}
