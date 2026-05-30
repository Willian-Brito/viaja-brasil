using FluentAssertions;
using Moq;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Application.Services;
using ViajaBrasil.Domain.Enums;
using ViajaBrasil.Domain.Exceptions;
using ViajaBrasil.Domain.Interfaces;
using Entities = ViajaBrasil.Domain.Entities;


namespace ViajaBrasil.Tests.Unit.TouristSpot.Services;

public class TouristSpotServiceTests
{
    private readonly Mock<ITouristSpotRepository> _touristSpotRepositoryMock;
    private readonly Mock<ICityRepository> _cityRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    private readonly TouristSpotService _service;

    public TouristSpotServiceTests()
    {
        _touristSpotRepositoryMock = new Mock<ITouristSpotRepository>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _touristSpotRepositoryMock
            .Setup(x => x.UnitOfWork)
            .Returns(_unitOfWorkMock.Object);

        _service = new TouristSpotService(
            _touristSpotRepositoryMock.Object,
            _cityRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve criar um ponto turístico quando os dados forem válidos")]
    public async Task CreateAsync_Should_Create_TouristSpot_When_Request_Is_Valid()
    {
        // Arrange
        var city = new Entities.City("3550308", "São Paulo", State.SP);

        var request = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu",
            Location = "Av. Paulista",
            CityIbgeCode = "3550308"
        };

        _cityRepositoryMock
            .Setup(x => x.GetByIbgeCodeAsync(request.CityIbgeCode))
            .ReturnsAsync(city);

        // Act
        var id = await _service.CreateAsync(request);

        // Assert
        id.Should().NotBeEmpty();

        _touristSpotRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Entities.TouristSpot>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Deve lançar exceção ao criar quando a cidade não existir")]
    public async Task CreateAsync_Should_Throw_When_City_Not_Found()
    {
        // Arrange
        var request = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu",
            Location = "Av. Paulista",
            CityIbgeCode = "9999999"
        };

        _cityRepositoryMock
            .Setup(x => x.GetByIbgeCodeAsync(request.CityIbgeCode))
            .ReturnsAsync((Entities.City?)null);

        // Act
        var action = async () => await _service.CreateAsync(request);

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("City not found.");
    }

    [Fact(DisplayName = "Deve atualizar um ponto turístico quando os dados forem válidos")]
    public async Task UpdateAsync_Should_Update_TouristSpot_When_Request_Is_Valid()
    {
        // Arrange
        var city = new Entities.City("3550308", "São Paulo", State.SP);

        var touristSpot = new Entities.TouristSpot(
            "Old",
            "Old Description",
            "Old Location",
            "3550308"
        );

        var request = new TouristSpotRequest
        {
            Name = "New Name",
            Description = "New Description",
            Location = "New Location",
            CityIbgeCode = "3550308"
        };

        _touristSpotRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(touristSpot);

        _cityRepositoryMock
            .Setup(x => x.GetByIbgeCodeAsync(request.CityIbgeCode))
            .ReturnsAsync(city);

        // Act
        await _service.UpdateAsync(Guid.NewGuid(), request);

        // Assert
        _touristSpotRepositoryMock.Verify(x => x.Update(It.IsAny<Entities.TouristSpot>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact(DisplayName = "Deve lançar exceção ao atualizar quando o ponto turístico não existir")]
    public async Task UpdateAsync_Should_Throw_When_TouristSpot_Not_Found()
    {
        // Arrange
        _touristSpotRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Entities.TouristSpot?)null);

        // Act
        var action = async () => await _service.UpdateAsync(Guid.NewGuid(), new TouristSpotRequest());

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Tourist spot not found.");
    }

    [Fact(DisplayName = "Deve lançar exceção ao atualizar quando a cidade não existir")]
    public async Task UpdateAsync_Should_Throw_When_City_Not_Found()
    {
        // Arrange
        var touristSpot = new Entities.TouristSpot(
            "Name",
            "Description",
            "Location",
            "3550308"
        );

        _touristSpotRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(touristSpot);

        _cityRepositoryMock
            .Setup(x => x.GetByIbgeCodeAsync(It.IsAny<string>()))
            .ReturnsAsync((Entities.City?)null);

        // Act
        var action = async () =>
            await _service.UpdateAsync(
                Guid.NewGuid(),
                new TouristSpotRequest
                {
                    CityIbgeCode = "9999999"
                });

        // Assert
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("City not found.");
    }

    [Fact(DisplayName = "Deve retornar um ponto turístico pelo identificador")]
    public async Task GetByIdAsync_Should_Return_TouristSpot()
    {
        // Arrange
        var city = new Entities.City("3550308", "São Paulo", State.SP);

        var touristSpot = new Entities.TouristSpot(
            "MASP",
            "Museu",
            "Av. Paulista",
            city.IbgeCode
        );

        touristSpot.Update(
            touristSpot.Name,
            touristSpot.Description,
            touristSpot.Location,
            city.IbgeCode,
            city
        );

        _touristSpotRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(touristSpot);

        // Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().NotBeNull();
    }
}