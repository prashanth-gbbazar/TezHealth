namespace TezHealth.Infrastructure;

using TezHealth.Application.Interfaces;
using TezHealth.Application.Services;
using TezHealth.Infrastructure.Persistence;
using TezHealth.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Add Drug Services
        services.AddScoped<IDrugRepository, DrugRepository>();
        services.AddScoped<IDrugService, DrugService>();

        // Add Vendor Services
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IVendorService, VendorService>();

        return services;
    }
}
