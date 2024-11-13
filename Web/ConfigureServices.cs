using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Connectors;
using Ichiba.Libs.DocumentSdk.Interface;
using Ichiba.Libs.DocumentSdk.Services;
using RestEase.HttpClientFactory;
using Web.Models;

namespace Web;
public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var restEaseClientConfig = configuration.GetSection("RestEaseClients").Get<RestEaseClientConfig>();

        services.AddRestEaseClient<ITemplateDocumentService>(restEaseClientConfig.TemplateDocumentServiceUrl)
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    })
    .AddHttpMessageHandler<ForwardWorkContextHttpMessageDelegateHandler>();

        services.AddRestEaseClient<IFileUploadService>(restEaseClientConfig.FileUploadServiceUrl)
            .AddHttpMessageHandler<ForwardWorkContextHttpMessageDelegateHandler>();
        services.AddScoped<IDocumentConnector, DocumentConnector>();

        services.AddScoped<IDocumentServiceFactory, DocumentServiceFactory>();
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IExcelService<ConcreteDocumentItem>, ExcelService<ConcreteDocumentItem>>();
        services.AddTransient<ForwardWorkContextHttpMessageDelegateHandler>();

        return services;
    }
}
