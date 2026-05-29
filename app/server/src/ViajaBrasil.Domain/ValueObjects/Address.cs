using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.Domain.ValueObjects;

public class Address
{
    public string Location { get; private set; }
    public string City { get; private set; }
    public State State { get; private set; }
    
    // EF Core
    protected Address() { }

    public Address(string location, string city, State state)
    {
        Validate(location, city);

        Location = location;
        City = city;
        State = state;
    }

    private void Validate(string location, string city)
    { 
        DomainException.When(string.IsNullOrWhiteSpace(location), "Location is required.");
        DomainException.When(string.IsNullOrWhiteSpace(city), "City is required.");
        DomainException.When(location.Length > 200, "Location must contain up to 200 characters.");
        DomainException.When(city.Length > 100, "City must contain up to 100 characters.");
    }
}