using Ichiba.Libs.DocumentSdk.Connectors;
using Ichiba.Libs.DocumentSdk.Models;
using RestEase;

namespace Web.Models
{
    public class DocumentConnector : IDocumentConnector
    {
        public Task<DocumentResponse> Export([Body] ExportSingleRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentResponse> Exports([Body] ExportMultipleRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<MergePdfDocumentsResponse> MergePdfDocuments([Body] MergePdfDocumentsRequest body, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
