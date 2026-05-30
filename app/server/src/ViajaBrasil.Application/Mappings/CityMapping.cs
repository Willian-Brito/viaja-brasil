using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Application.Mappings;

public static class CityMapping
{
    public static CityResponse ToResponse(this City city)
    {
        return new CityResponse
        {
            IbgeCode = city.IbgeCode,
            Name = city.Name
        };
    }
}