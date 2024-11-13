using Ichiba.Libs.DocumentSdk.Models;
using RestEase;

namespace Ichiba.Libs.DocumentSdk.Interface;

public interface ITemplateDocumentService
{
    [Get("/api/templates/{reportCode}")]
    Task<TemplateData> GetTemplateDataAsync(
             [Path] string reportCode,
             [Query] Guid workspaceId,
             CancellationToken cancellationToken);
    [Get("/api/templates/{reportCode}")]
    Task<HttpResponseMessage> GetTemplateFileAsync(
    [Path] string reportCode,
    [Query] Guid workspaceId,
    CancellationToken cancellationToken);

    [Post("/api/report-histories")]
    Task CreateHistoryAsync([Body] ReportHistory historyRequest, CancellationToken cancellationToken);
    [Post("/files/public")]
    Task<FileUploadResponse> UploadFileToPublicAsync(
           [Body] MultipartFormDataContent content,
           CancellationToken cancellationToken);
    [Post("api/export/singlefile")]
    Task<DocumentResponse> ExportDocumentAsync([Body] ExportTemplateRequest exportCommand, CancellationToken cancellationToken);
}
