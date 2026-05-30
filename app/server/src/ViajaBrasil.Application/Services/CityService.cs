using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Mappings;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.Application.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IReadOnlyCollection<CityResponse>> GetByStateAsync(string state)
    {
        if (!Enum.TryParse<State>(state, true, out var stateEnum))
            throw new DomainException("Invalid state.");

        var cities = await _cityRepository.GetByStateAsync(stateEnum);

        return cities.Select(x => x.ToResponse()).ToList();
    }
}