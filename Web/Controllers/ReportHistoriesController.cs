using Microsoft.AspNetCore.Mvc;
using Web.Interface;
using Web.Models;

namespace Web.Controllers;

[Route("api/report-histories")]
public class ReportHistoriesController : ControllerBase
{
    private readonly IReportHistoryDocumentService _reportHistoryDocumentService;

    public ReportHistoriesController(IReportHistoryDocumentService reportHistoryDocumentService)
    {
        _reportHistoryDocumentService = reportHistoryDocumentService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateReportHistory([FromBody] ReportHistory reportHistory)
    {
        try
        {
            var result = await _reportHistoryDocumentService.CreateReportHistoryAsync(reportHistory);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
