using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using RestEase.Implementation;
using Web.Models;

namespace Web.Controllers;

[Route("api/reports")]  
public class ReportsController : ControllerBase
{
    private readonly IExcelService<ConcreteDocumentItem> _excelService;
    public ReportsController(IExcelService<ConcreteDocumentItem> excelService)
    {
        _excelService = excelService;
    }
    [HttpGet]
    public async Task<IActionResult> GetReportHistoryWithPagination([FromQuery] ReportHistoryPagination query, CancellationToken cancellationToken)
    {
        var response = await _excelService.GetReportHistory(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> WriteDocument([FromBody] ExportTemplateRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _excelService.ExportAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

