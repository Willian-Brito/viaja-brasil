using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Domain.Interfaces;

public interface ITouristSpotRepository : IRepository<TouristSpot>
{
    Task<TouristSpot?> GetByIdAsync(Guid id);
    Task<IEnumerable<TouristSpot>> GetAllAsync(string? search, int page, int pageSize);
}