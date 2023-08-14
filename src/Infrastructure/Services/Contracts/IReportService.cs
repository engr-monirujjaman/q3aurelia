using Infrastructure.DTOs;

namespace Infrastructure.Services.Contracts;

public interface IReportService
{
    Task<FinancialReportDto> GenerateFinancialReport(Guid clientId, DateTime startDate, DateTime endDate);
}
