using Microsoft.EntityFrameworkCore;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Interfaces;

namespace ViajaBrasil.Infrastructure.Context;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<TouristSpot> TouristSpots { get; set; }
    public DbSet<City> Cities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}