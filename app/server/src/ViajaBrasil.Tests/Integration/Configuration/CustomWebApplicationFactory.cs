using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViajaBrasil.Infrastructure.Context;

namespace ViajaBrasil.Tests.Integration.Configuration;

public class CustomWebApplicationFactory : WebApplicationFactory<API.Program>
{
    private SqliteConnection _connection = null!;
        
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AppDbContext>)
            );

            if (descriptor != null)
                services.Remove(descriptor);
            
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });
    }
}