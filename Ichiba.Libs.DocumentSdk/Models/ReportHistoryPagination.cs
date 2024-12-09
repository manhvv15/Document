namespace Ichiba.Libs.DocumentSdk.Models;

public class ReportHistoryPagination
{
    public string? UserProfileId { get; set; }
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}
