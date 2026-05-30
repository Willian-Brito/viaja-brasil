using FluentAssertions;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;
using Entities = ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Tests.Unit.City.Domain;

public class CityTests
{
    [Fact(DisplayName = "Deve criar uma cidade quando os dados forem válidos")]
    public void Constructor_Should_Create_City_When_Data_Is_Valid()
    {
        // Arrange & Act
        var city = new Entities.City("3550308", "São Paulo", State.SP);

        // Assert
        city.IbgeCode.Should().Be("3550308");
        city.Name.Should().Be("São Paulo");
        city.State.Should().Be(State.SP);
    }

    [Theory(DisplayName = "Deve lançar exceção quando o código IBGE não for informado")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_IbgeCode_Is_Invalid(string ibgeCode)
    {
        // Act
        var action = () => new Entities.City(ibgeCode, "São Paulo", State.SP);

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage("IBGE code is required.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando o código IBGE não possuir 7 dígitos")]
    [InlineData("123")]
    [InlineData("123456")]
    [InlineData("12345678")]
    public void Constructor_Should_Throw_When_IbgeCode_Length_Is_Invalid(
        string ibgeCode)
    {
        var action = () => new Entities.City(ibgeCode, "São Paulo", State.SP);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("IBGE code must contain 7 digits.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando o código IBGE possuir caracteres não numéricos")]
    [InlineData("ABC1234")]
    [InlineData("ABC1234")]
    [InlineData("12345AB")]
    [InlineData("ABCDEFG")]
    public void Constructor_Should_Throw_When_IbgeCode_Is_Not_Numeric(
        string ibgeCode)
    {
        var action = () => new Entities.City(ibgeCode, "São Paulo", State.SP);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("IBGE code must contain only numbers.");
    }

    [Theory(DisplayName = "Deve lançar exceção quando o nome da cidade não for informado")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Constructor_Should_Throw_When_Name_Is_Invalid(
        string name)
    {
        var action = () => new Entities.City("3550308", name, State.SP);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("City name is required.");
    }

    [Fact(DisplayName = "Deve lançar exceção quando o nome da cidade ultrapassar 100 caracteres")]
    public void Constructor_Should_Throw_When_Name_Exceeds_100_Characters()
    {
        var name = new string('A', 101);
        var action = () => new Entities.City("3550308", name, State.SP);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("City name must contain up to 100 characters.");
    }
}