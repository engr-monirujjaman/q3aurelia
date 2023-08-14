using Infrastructure.Contexts;
using Infrastructure.Contexts.Contracts;
using Infrastructure.Services;
using Infrastructure.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite("Data Source=data.db;");
        });

        services.AddScoped<IAppDbContext>(x => x.GetRequiredService<AppDbContext>());

        services.AddScoped<ITransactionService, TransactionService>();
        
        return services;
    }
}
