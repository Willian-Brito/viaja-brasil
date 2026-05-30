using Microsoft.OpenApi.Models;

namespace ViajaBrasil.API.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Evertec - Pontos Turísticos API", 
                Description = "Desenvolvido por Willian Brito",
                Version = "v1",
                Contact = new OpenApiContact { Name = "Willian Brito", Email = "contato@evertec.com" },
                License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            });
        });

        return services;
    }
    
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });

        return app;
    }
}