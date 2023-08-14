using Infrastructure.DTOs;
using Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;

    }
    
    [HttpGet("financial/{clientId:guid}")]
    public async Task<IResult> GenerateFinancialReport(Guid clientId, DateTime startDate, DateTime endDate)
    {
        var report = await _reportService.GenerateFinancialReport(clientId, startDate, endDate);
        return report is null ? Results.NotFound() : Results.Ok(report);
    }
}
