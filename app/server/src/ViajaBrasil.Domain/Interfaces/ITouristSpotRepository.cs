using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Domain.Interfaces;

public interface ITouristSpotRepository
{
    Task AddAsync(TouristSpot touristSpot);
    Task<TouristSpot?> GetByIdAsync(Guid id);
    Task<IEnumerable<TouristSpot>> GetAllAsync(string? search, int page, int pageSize);
}