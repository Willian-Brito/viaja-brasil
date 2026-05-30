using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.Domain.Entities;

public class City
{
    public string IbgeCode { get; private set; }
    public string Name { get; private set; }
    public State State { get; private set; }
    public ICollection<TouristSpot> TouristSpots { get; private set; } = [];
    
    // EF Core
    protected City() { }
    
    public City(string ibgeCode, string name, State state)
    {
        Validate(ibgeCode, name);

        IbgeCode = ibgeCode;
        Name = name.Trim();
        State = state;
    }
    
    private static void Validate(string ibgeCode, string name)
    {
        DomainException.When(string.IsNullOrWhiteSpace(ibgeCode), "IBGE code is required.");
        DomainException.When(ibgeCode.Length != 7, "IBGE code must contain 7 digits.");
        DomainException.When(!ibgeCode.All(char.IsDigit), "IBGE code must contain only numbers.");
        DomainException.When(string.IsNullOrWhiteSpace(name), "City name is required.");
        DomainException.When(name.Length > 100, "City name must contain up to 100 characters.");
    }
}