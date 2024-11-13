using Ichiba.Libs.DocumentSdk.Enums;

namespace Ichiba.Libs.DocumentSdk.Models;

public class ExportTemplateRequest
{
    public string ReportCode { get; set; } = string.Empty;
    public Guid WorkspaceId { get; set; } = Guid.Empty;
    public string UserProfileId { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public Dictionary<string, ImageDetail> Images { get; set; } = new Dictionary<string, ImageDetail>();
    public Dictionary<string, BarCodeDetail> BarCodes { get; set; } = new Dictionary<string, BarCodeDetail>();
    public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
    public List<GroupColumnItem?> ColumnGroups { get; set; }

    public List<ExcelErrorModel> Errors { get; set; }

    public ExportType ExportType()
    {
        var names = Enum.GetNames(typeof(ExportType));
        if (names.Any(x => x.ToLower().Equals(this.FileExtension.ToLower())))
        {
            return Enum.Parse<ExportType>(FileExtension, true);
        }

        throw new ApplicationException();
    }
    public TemplateType RequestType()
    {
        var names = Enum.GetNames(typeof(TemplateType));
        if (names.Any(x => x.ToLower().Equals(this.FileType.ToLower())))
        {
            return Enum.Parse<TemplateType>(FileType, true);
        }

        throw new ApplicationException();
    }
}

