using System.Collections;
using Microsoft.EntityFrameworkCore;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Infrastructure.Context;

namespace ViajaBrasil.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<City?> GetByIbgeCodeAsync(string ibgeCode)
    {
        return await _context.Cities.FirstOrDefaultAsync(x => x.IbgeCode == ibgeCode);
    }

    public async Task<IReadOnlyCollection<City>> GetByStateAsync(State state)
    {
        return await _context.Cities
            .AsNoTracking()
            .Where(x => x.State == state)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}