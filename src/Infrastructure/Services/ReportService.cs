using Infrastructure.Contexts.Contracts;
using Infrastructure.DTOs;
using Infrastructure.Services.Contracts;

namespace Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly IAppDbContext _dbContext;

    public ReportService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public Task<FinancialReportDto> GenerateFinancialReport(Guid clientId, DateTime startDate, DateTime endDate)
    {
        return Task.FromResult(new FinancialReportDto(100, 200, 300));
    }
}
