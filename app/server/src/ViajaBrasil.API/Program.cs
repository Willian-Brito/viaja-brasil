using FluentValidation.AspNetCore;
using ViajaBrasil.API.Configuration;
using ViajaBrasil.API.Middlewares;
using ViajaBrasil.Application.DependencyInjection;
using ViajaBrasil.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureSwagger();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwaggerConfiguration();

// app.UseHttpsRedirection();
app.ApplyMigrations();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();

namespace ViajaBrasil.API
{
    public partial class Program;
}
