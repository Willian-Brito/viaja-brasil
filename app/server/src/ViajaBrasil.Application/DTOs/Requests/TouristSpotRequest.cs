namespace ViajaBrasil.Application.DTOs.Requests;

public class TouristSpotRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string CityIbgeCode { get; set; } = string.Empty;
}