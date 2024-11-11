using Ichiba.Libs.DocumentSdk.Abstractions;
using RestEase.HttpClientFactory;
using Ichiba.Libs.DocumentSdk.Services;
using Ichiba.Libs.DocumentSdk.Connectors;
using Web.Models;
using Ichiba.Libs.DocumentSdk.Interface; 
using Microsoft.OpenApi.Models;


namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                options.OperationFilter<FileUploadOperationFilter>();
            });

            builder.Services.AddHttpClient();
            builder.Services.AddRestEaseClient<ITemplateDocumentService>("https://localhost:5111");
            builder.Services.AddRestEaseClient<IFileUploadService>("http://localhost:8068");
            //builder.Services.AddRestEaseClient<IReportHistoryDocumentService>("https://localhost:5111");
            builder.Services.AddScoped<IDocumentConnector, DocumentConnector>();

            builder.Services.AddScoped<IDocumentServiceFactory, DocumentServiceFactory>();
            builder.Services.AddScoped<IWordService, WordService>();  
            builder.Services.AddScoped<IPdfService, PdfService>();
            builder.Services.AddScoped<IExcelService<ConcreteDocumentItem>, ExcelService<ConcreteDocumentItem>>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
