using Microsoft.EntityFrameworkCore;
using ViajaBrasil.Infrastructure.Context;

namespace ViajaBrasil.API.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfig(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        return services;
    }
    
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {            
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();   
            context.Database.Migrate();
        }
    }
}