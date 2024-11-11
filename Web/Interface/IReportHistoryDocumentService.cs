using RestEase;
using Web.Models;

namespace Web.Interface;

public interface IReportHistoryDocumentService
{
    [Post("/api/report-histories")]
    Task<Guid> CreateReportHistoryAsync([Body] ReportHistory reportHistory);
}
