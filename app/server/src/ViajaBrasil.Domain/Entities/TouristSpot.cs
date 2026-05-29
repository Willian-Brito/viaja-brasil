using ViajaBrasil.Domain.Common;
using ViajaBrasil.Domain.Exceptions;
using ViajaBrasil.Domain.ValueObjects;

namespace ViajaBrasil.Domain.Entities;

public class TouristSpot : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // EF Core
    protected TouristSpot() { }

    public TouristSpot(string name, string description, Address address)
    {
        Validate(name, description);

        Name = name;
        Description = description;
        Address = address;
        CreatedAt = DateTime.Now;
    }

    public void Update(string name, string description, Address address)
    {
        Validate(name, description);

        Name = name;
        Description = description;
        Address = address;
    }

    private void Validate(string name, string description)
    {
        DomainException.When(string.IsNullOrWhiteSpace(name), "Name is required.");
        DomainException.When(string.IsNullOrWhiteSpace(description), "Description is required.");
        DomainException.When(name.Length > 150, "Name must contain up to 150 characters.");
        DomainException.When(description.Length > 100, "Description must contain up to 100 characters.");
    }
}