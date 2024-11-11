using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Interface;
using Ichiba.Libs.DocumentSdk.Models;

namespace Ichiba.Libs.DocumentSdk.Services;

public class ExcelService<T>(IHttpClientFactory httpClientFactory, ITemplateDocumentService templateService,IFileUploadService fileUploadService, IEnumerable<IDocumentValidator<T>> validators)
    : BaseDocument<T>(httpClientFactory, templateService, fileUploadService, validators), IExcelService<T>
    where T : DocumentItemBase, new()
{
     public async Task<ImportExcelResponse<T>> ReadAsync(string filePath, ImportExcelRequest request, CancellationToken cancellationToken = default) => await ReadFileAsync(filePath, request, cancellationToken);

    public async Task<ImportExcelResponse<T>> ReadAsync(Stream file, ImportExcelRequest request, CancellationToken cancellationToken = default) => await ReadFileAsync(file, request, cancellationToken);

    public async Task<DocumentResponse> WriteAsync(ExportSingleRequest request, CancellationToken cancellationToken = default)
    {
        var document = await WriteFileAsync(request, cancellationToken);
        return new DocumentResponse()
        {
            Success = true,
            FileName = request.FileName,
            FileExtension = request.FileExtension,
            Data = document
        };
    }
    public async Task<DocumentResponse> ExportAsync(ExportTemplateRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var templateStream = await GetTemplateFileStreamAsync(request.ReportCode, request.WorkspaceId, cancellationToken);

        byte[] document;
        using (var outputStream = new MemoryStream())
        {
            await templateStream.CopyToAsync(outputStream, cancellationToken);
            document = outputStream.ToArray();
        }
        var templateData = await GetTemplateDataAsync(request.ReportCode, request.WorkspaceId, cancellationToken);

        var fileName = $"{request.ReportCode}_{DateTime.UtcNow:yyyyMMddHHmmssfff}.{templateData.Type.ToLower()}";

        var uploadResponse = await UploadFileToPublicAsync(new MemoryStream(document), fileName, cancellationToken);
        string uri = uploadResponse?.Uri;

        var response = new DocumentResponse
        {
            Success = uploadResponse != null,
            FileName = fileName,
            FileExtension = Path.GetExtension(fileName),
            Data = document
        };

        await CreateHistoryAsync(new ReportHistory
        {
            UserProfileId = request.UserProfileId,
            ReportCode = request.ReportCode,
            Link = uri
        }, cancellationToken);

        return response;
    }

    public async Task<DocumentResponse> WriteAsync(Stream file, ExportSingleRequest request, CancellationToken cancellationToken = default)
    {
        var document = await WriteFileAsync(request, cancellationToken);
        return new DocumentResponse()
        {
            Success = true,
            FileName = request.FileName,
            FileExtension = request.FileExtension,
            Data = document
        };
    }

    public async Task<DocumentResponse> WriteErrorAsync(Stream file, ExportSingleRequest request, CancellationToken cancellationToken = default)
    {
        var document = await WriteFileErrorAsync(file, request, cancellationToken);
        return new DocumentResponse()
        {
            Success = true,
            FileName = request.FileName,
            FileExtension = request.FileExtension,
            Data = document
        };
    }
}
