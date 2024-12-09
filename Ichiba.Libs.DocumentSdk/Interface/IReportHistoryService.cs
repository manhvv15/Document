using Ichiba.Libs.DocumentSdk.Models;
using RestEase;

namespace Ichiba.Libs.DocumentSdk.Interface;

public interface IReportHistoryService
{
    [Get("api/report-histories")]
    Task<PageResult<ReportHistory>> ReportHistoryWithPagination(ReportHistoryPagination query ,CancellationToken cancellationToken);
}
