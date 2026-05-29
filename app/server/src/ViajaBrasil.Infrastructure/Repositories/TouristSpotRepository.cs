using Microsoft.EntityFrameworkCore;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Interfaces;
using ViajaBrasil.Infrastructure.Context;

namespace ViajaBrasil.Infrastructure.Repositories;

public class TouristSpotRepository : ITouristSpotRepository
{
    private readonly AppDbContext _context;
    public IUnitOfWork UnitOfWork => _context;
    

    public TouristSpotRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TouristSpot touristSpot)
    {
        await _context.TouristSpots.AddAsync(touristSpot);
    }

    public void Update(TouristSpot touristSpot)
    {
        _context.TouristSpots.Update(touristSpot);
    }

    public void Remove(TouristSpot entity)
    {
        _context.Remove(entity);
    }

    public async Task<TouristSpot?> GetByIdAsync(Guid id)
    {
        return await _context.TouristSpots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<TouristSpot>> GetAllAsync(string? search, int page, int pageSize)
    {
        var query = _context.TouristSpots
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim().ToLower();

            query = query.Where(x =>
                x.Name.ToLower().Contains(search)
                || x.Description.ToLower().Contains(search)
                || x.Address.Location.ToLower().Contains(search));
        }

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}