using Ichiba.Libs.DocumentSdk.Abstractions;
using Ichiba.Libs.DocumentSdk.Models;
using Microsoft.AspNetCore.Mvc;
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
    [HttpPost]
    public async Task<IActionResult> WriteDocument([FromBody] ExportTemplateRequest request, CancellationToken cancellationToken)
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

