using ViajaBrasil.Application.DTOs.Response;

namespace ViajaBrasil.Application.Interfaces;

public interface ICityService
{
    Task<IReadOnlyCollection<CityResponse>> GetByStateAsync(string state);
}