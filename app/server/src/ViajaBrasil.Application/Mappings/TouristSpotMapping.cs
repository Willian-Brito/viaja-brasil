using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Application.Mappings;

public static class TouristSpotMapping
{
    public static TouristSpotResponse ToResponse(this TouristSpot touristSpot)
    {
        return new TouristSpotResponse
        {
            Id = touristSpot.Id,
            Name = touristSpot.Name,
            Description = touristSpot.Description,
            Location = touristSpot.Address.Location,
            City = touristSpot.Address.City,
            State = touristSpot.Address.State.ToString(),
            CreatedAt = touristSpot.CreatedAt
        };
    }
}