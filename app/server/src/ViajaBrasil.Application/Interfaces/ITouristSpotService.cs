using ViajaBrasil.Application.Common;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.DTOs.Response;

namespace ViajaBrasil.Application.Interfaces;

public interface ITouristSpotService
{
    Task<Guid> CreateAsync(TouristSpotRequest request);
    Task UpdateAsync(Guid id, TouristSpotRequest request);
    Task<TouristSpotResponse?> GetByIdAsync(Guid id);
    Task<PagedResult<TouristSpotResponse>> GetAllAsync(string? search, int page, int pageSize);
}