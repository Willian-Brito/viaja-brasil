using FluentAssertions;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;
using Entities = ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Tests.Unit.TouristSpot.Domain;

public class TouristSpotTests
{
    [Fact(DisplayName = "Deve lançar exceção quando o nome da cidade ultrapassar 100 caracteres")]
    public void Constructor_Should_Create_TouristSpot_When_Data_Is_Valid()
    {
        // Arrange & Act
        var touristSpot = new Entities.TouristSpot(
            "Cristo Redentor",
            "Ponto turístico famoso",
            "Rio de Janeiro",
            "3304557"
        );

        // Assert
        touristSpot.Name.Should().Be("Cristo Redentor");
        touristSpot.Description.Should().Be("Ponto turístico famoso");
        touristSpot.Location.Should().Be("Rio de Janeiro");
        touristSpot.CityIbgeCode.Should().Be("3304557");
        touristSpot.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Theory(DisplayName = "Deve lançar exceção quando o nome do ponto turístico não for informado")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_Name_Is_Invalid(
        string name)
    {
        var action = () => new Entities.TouristSpot(
            name,
            "Description",
            "Location",
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Name is required.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o nome do ponto turístico ultrapassar 150 caracteres")]
    public void Constructor_Should_Throw_When_Name_Exceeds_150_Characters()
    {
        var action = () => new Entities.TouristSpot(
            new string('A', 151),
            "Description",
            "Location",
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Name must contain up to 150 characters.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando a descrição não for informada")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_Description_Is_Invalid(
        string description)
    {
        var action = () => new Entities.TouristSpot(
            "Name",
            description,
            "Location",
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Description is required.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando a descrição ultrapassar 100 caracteres")]
    public void Constructor_Should_Throw_When_Description_Exceeds_100_Characters()
    {
        var action = () => new Entities.TouristSpot(
            "Name",
            new string('A', 101),
            "Location",
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Description must contain up to 100 characters.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando a localização não for informada")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_Location_Is_Invalid(
        string location)
    {
        var action = () => new Entities.TouristSpot(
            "Name",
            "Description",
            location,
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Location reference is required.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando a localização ultrapassar 200 caracteres")]
    public void Constructor_Should_Throw_When_Location_Exceeds_200_Characters()
    {
        var action = () => new Entities.TouristSpot(
            "Name",
            "Description",
            new string('A', 201),
            "3550308"
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Location reference must contain up to 200 characters.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando o código IBGE da cidade não for informado")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_CityIbgeCode_Is_Invalid(
        string cityIbgeCode)
    {
        var action = () => new Entities.TouristSpot(
            "Name",
            "Description",
            "Location",
            cityIbgeCode
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("City IBGE code is required.");
    }

    [Fact(DisplayName = "Deve atualizar os dados do ponto turístico quando as informações forem válidas")]
    public void Update_Should_Update_Entity_When_Data_Is_Valid()
    {
        // Arrange
        var city = new Entities.City("3550308", "São Paulo", State.SP);

        var touristSpot = new Entities.TouristSpot(
            "Old Name",
            "Old Description",
            "Old Location",
            "3550308"
        );

        // Act
        touristSpot.Update(
            "New Name",
            "New Description",
            "New Location",
            "3304557",
            city
        );

        // Assert
        touristSpot.Name.Should().Be("New Name");
        touristSpot.Description.Should().Be("New Description");
        touristSpot.Location.Should().Be("New Location");
        touristSpot.CityIbgeCode.Should().Be("3304557");
        touristSpot.City.Should().Be(city);
    }
}