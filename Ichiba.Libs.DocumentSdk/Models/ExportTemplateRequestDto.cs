namespace Ichiba.Libs.DocumentSdk.Models;

public class ExportTemplateRequestDto
{
    public string ReportCode { get; set; } = string.Empty;
    public Guid WorkspaceId { get; set; } = Guid.Empty;
    public string UserProfileId { get; set; }
    public Dictionary<string, ImageDetail> Images { get; set; } = new Dictionary<string, ImageDetail>();
    public Dictionary<string, BarCodeDetail> BarCodes { get; set; } = new Dictionary<string, BarCodeDetail>();
    public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    public List<GroupColumnItem?> ColumnGroups { get; set; }
}
