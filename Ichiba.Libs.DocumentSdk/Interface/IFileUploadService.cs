using Ichiba.Libs.DocumentSdk.Models;
using RestEase;

namespace Ichiba.Libs.DocumentSdk.Interface;

public interface IFileUploadService
{
    [Post("/files/public")]
    Task<FileUploadResponse> UploadFileAsync(
            [Body] MultipartFormDataContent content,
            CancellationToken cancellationToken);
}
