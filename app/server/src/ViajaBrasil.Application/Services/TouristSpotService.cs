using ViajaBrasil.Application.Common;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Mappings;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Exceptions;
using ViajaBrasil.Domain.Interfaces;

namespace ViajaBrasil.Application.Services;

public class TouristSpotService : ITouristSpotService
{
    private readonly ITouristSpotRepository _touristSpotRepository;
    private readonly ICityRepository _cityRepository;

    public TouristSpotService(ITouristSpotRepository touristSpotRepository, ICityRepository cityRepository)
    {
        _touristSpotRepository = touristSpotRepository;
        _cityRepository = cityRepository;
    }
    
    public async Task<Guid> CreateAsync(TouristSpotRequest request)
    {
        var city = await _cityRepository.GetByIbgeCodeAsync(request.CityIbgeCode);

        if (city is null)
            throw new DomainException("City not found.");
        
        var touristSpot = new TouristSpot(request.Name, request.Description, request.Location, request.CityIbgeCode);

        await _touristSpotRepository.AddAsync(touristSpot);
        await _touristSpotRepository.UnitOfWork.Commit();
            
        return touristSpot.Id;
    }

    public async Task UpdateAsync(Guid id, TouristSpotRequest request)
    {
        var touristSpot = await _touristSpotRepository.GetByIdAsync(id);
        
        if (touristSpot is null)
            throw new DomainException("Tourist spot not found.");

        var city = await _cityRepository.GetByIbgeCodeAsync(request.CityIbgeCode);

        if (city is null)
            throw new DomainException("City not found.");
        
        touristSpot.Update(request.Name, request.Description, request.Location, request.CityIbgeCode, city);

        _touristSpotRepository.Update(touristSpot);
        await _touristSpotRepository.UnitOfWork.Commit();
    }

    public async Task<TouristSpotResponse?> GetByIdAsync(Guid id)
    {
        var touristSpot = await _touristSpotRepository.GetByIdAsync(id);
        return touristSpot?.ToResponse();
    }

    public async Task<PagedResult<TouristSpotResponse>> GetAllAsync(string? search, int page, int pageSize)
    {
        var touristSpots = await _touristSpotRepository.GetAllAsync(search, page, pageSize);
        var dtos = touristSpots.Select(i => i.ToResponse());
        var pagedResult = dtos.ToPagedList(page, pageSize);
        
        return pagedResult;
    }
}