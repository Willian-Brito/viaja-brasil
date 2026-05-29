using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Services;
using ViajaBrasil.Application.Validators;

namespace ViajaBrasil.Application.DependencyInjection;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITouristSpotService, TouristSpotService>();
        services.AddValidatorsFromAssemblyContaining<TouristSpotValidator>();

        return services;
    }
}