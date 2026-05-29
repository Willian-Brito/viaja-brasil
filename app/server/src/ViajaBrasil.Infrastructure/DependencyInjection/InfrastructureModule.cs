using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViajaBrasil.Domain.Interfaces;
using ViajaBrasil.Infrastructure.Context;
using ViajaBrasil.Infrastructure.Repositories;

namespace ViajaBrasil.Infrastructure.DependencyInjection;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<ITouristSpotRepository, TouristSpotRepository>();
        return services;
    }
}