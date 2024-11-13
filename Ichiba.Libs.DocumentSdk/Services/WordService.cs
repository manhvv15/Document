using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Connectors;
using Ichiba.Libs.DocumentSdk.Models;

namespace Ichiba.Libs.DocumentSdk.Services;

public class WordService(IDocumentConnector documentConnector) : IWordService
{
    public Task<DocumentResponse> ExportReportAsync(ExportTemplateRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<DocumentResponse> WriteAsync(ExportSingleRequest request)
    {
        return await documentConnector.Export(request);
    }

    public async Task<DocumentResponse> WriteAsync(ExportMultipleRequest request)
    {
        return await documentConnector.Exports(request);
    }
}
