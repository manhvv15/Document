namespace Ichiba.Libs.DocumentSdk.Models;

public class ExportTemplateRequest
{
    public string ReportCode { get; set; } = string.Empty;
    public Guid WorkspaceId { get; set; } = Guid.Empty;
    public string UserProfileId { get; set; }
}

