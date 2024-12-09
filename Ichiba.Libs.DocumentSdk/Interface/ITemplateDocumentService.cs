using Ichiba.Libs.DocumentSdk.Models;
using RestEase;

namespace Ichiba.Libs.DocumentSdk.Interface;

public interface ITemplateDocumentService
{
    [Post("/api/report-histories")]
    Task CreateHistoryAsync([Body] ReportHistoryDto historyRequest, CancellationToken cancellationToken);
    [Post("api/files/export/template")]
    Task<DocumentResponse> ExportTemplateAsync([Body] ExportTemplateRequestDto exportCommand, CancellationToken cancellationToken);
}
