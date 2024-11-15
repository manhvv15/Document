using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Constants;
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
    public async Task<DocumentResponse> ExportAsync(ExportTemplateRequestDto request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var documentResponse = await ExportTemplateAsync(request, cancellationToken);

        if (documentResponse == null || !documentResponse.Success || documentResponse.Data == null)
        {
            throw new ApplicationException(ErrorMessageConstants.FailedSingleFile);
        }

        var uploadResponse = await UploadFileToPublicAsync(new MemoryStream(documentResponse.Data), documentResponse.FileName, cancellationToken);
        string uri = uploadResponse?.Uri;

        if (string.IsNullOrEmpty(uri))
        {
            throw new ApplicationException(ErrorMessageConstants.FailUploadFile);
        }

        await CreateHistoryAsync(new ReportHistory
        {
            UserProfileId = request.UserProfileId,
            ReportCode = request.ReportCode,
            WorkspaceId = request.WorkspaceId,
            Link = uri
        }, cancellationToken);

        return new DocumentResponse
        {
            Success = true,
            FileName = documentResponse.FileName,
            FileExtension = documentResponse.FileExtension,
            Data = documentResponse.Data
        };
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
