using ViajaBrasil.Domain.Common;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.Domain.Entities;

public class TouristSpot : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public string CityIbgeCode { get; private set; }
    public City City { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    // EF Core
    protected TouristSpot() { }

    public TouristSpot(string name, string description, string location, string cityIbgeCode)
    {
        Validate(name, description, location, cityIbgeCode);

        Name = name.Trim();
        Description = description.Trim();
        Location = location.Trim();
        CityIbgeCode = cityIbgeCode.Trim();
        
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string description, string location, string cityIbgeCode, City city)
    {
        Validate(name, description, location, cityIbgeCode);

        Name = name.Trim();
        Description = description.Trim();
        Location = location.Trim();
        CityIbgeCode = cityIbgeCode.Trim();
        City = city;
    }

    private void Validate(string name, string description, string location, string cityIbgeCode)
    {
        DomainException.When(string.IsNullOrWhiteSpace(name), "Name is required.");
        DomainException.When(name.Length > 150, "Name must contain up to 150 characters.");
        DomainException.When(string.IsNullOrWhiteSpace(description), "Description is required.");
        DomainException.When(description.Length > 100, "Description must contain up to 100 characters.");
        DomainException.When(string.IsNullOrWhiteSpace(location), "Location reference is required.");
        DomainException.When(location.Length > 200, "Location reference must contain up to 200 characters.");
        DomainException.When(string.IsNullOrWhiteSpace(cityIbgeCode), "City IBGE code is required.");
        DomainException.When(cityIbgeCode.Length != 7, "City IBGE code must contain 7 digits.");
        DomainException.When(!cityIbgeCode.All(char.IsDigit), "City IBGE code must contain only numbers.");
    }
}