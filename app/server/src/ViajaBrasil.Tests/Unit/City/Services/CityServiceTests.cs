using FluentAssertions;
using Moq;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Services;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;

namespace ViajaBrasil.Tests.Unit.City.Services;

public class CityServiceTests
{
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly CityService _service;

    public CityServiceTests()
    {
        _cityRepositoryMock = new Mock<ICityRepository>();
        _service = new CityService(_cityRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve retornar cidades de um estado válido")]
    public async Task GetByStateAsync_Should_Return_Cities_When_State_Is_Valid()
    {
        // Arrange
        var cities = new List<ViajaBrasil.Domain.Entities.City>
        {
            new("3550308", "São Paulo", State.SP),
            new("3509502", "Campinas", State.SP)
        };

        _cityRepositoryMock
            .Setup(x => x.GetByStateAsync(State.SP))
            .ReturnsAsync(cities);

        // Act
        var result = await _service.GetByStateAsync("SP");

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact(DisplayName = "Deve lançar exceção quando o estado for inválido")]
    public async Task GetByStateAsync_Should_Throw_When_State_Is_Invalid()
    {
        // Act
        var action = async () =>
            await _service.GetByStateAsync("XX");

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Invalid state.");
    }
}