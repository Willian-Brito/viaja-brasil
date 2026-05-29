using ViajaBrasil.Application.Common;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Mappings;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;
using ViajaBrasil.Domain.Interfaces;
using ViajaBrasil.Domain.ValueObjects;

namespace ViajaBrasil.Application.Services;

public class TouristSpotService : ITouristSpotService
{
    private readonly ITouristSpotRepository _repository;

    public TouristSpotService(ITouristSpotRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Guid> CreateAsync(TouristSpotRequest request)
    {
        if (!Enum.TryParse<State>(request.State, true, out var state))
            throw new DomainException("Invalid state.");

        var address = new Address(request.Location, request.City, state);
        var touristSpot = new TouristSpot(request.Name, request.Description, address);

        await _repository.AddAsync(touristSpot);
        await _repository.UnitOfWork.Commit();
            
        return touristSpot.Id;
    }

    public async Task UpdateAsync(Guid id, TouristSpotRequest request)
    {
        var touristSpot = await _repository.GetByIdAsync(id);

        if (touristSpot is null)
            throw new DomainException("Tourist spot not found.");

        if (!Enum.TryParse<State>(request.State, true, out var state))
            throw new DomainException("Invalid state.");

        var address = new Address(request.Location, request.City, state);
        touristSpot.Update(request.Name, request.Description, address);

        _repository.Update(touristSpot);
        await _repository.UnitOfWork.Commit();
    }

    public async Task<TouristSpotResponse?> GetByIdAsync(Guid id)
    {
        var touristSpot = await _repository.GetByIdAsync(id);
        return touristSpot?.ToResponse();
    }

    public async Task<PagedResult<TouristSpotResponse>> GetAllAsync(string? search, int page, int pageSize)
    {
        var touristSpots = await _repository.GetAllAsync(search, page, pageSize);
        var dtos = touristSpots.Select(i => i.ToResponse());
        var pagedResult = dtos.ToPagedList(page, pageSize);
        
        return pagedResult;
    }
}