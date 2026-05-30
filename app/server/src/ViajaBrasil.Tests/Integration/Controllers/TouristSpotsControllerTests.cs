using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.DTOs.Response;
using ViajaBrasil.Tests.Integration.Configuration;
using ViajaBrasil.Tests.Integration.Contracts.Response;

namespace ViajaBrasil.Tests.Integration.Controllers;

public class TouristSpotsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TouristSpotsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "Deve criar um ponto turístico")]
    public async Task Create_Should_Return_Created()
    {
        // Arrange
        var request = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu de Arte",
            Location = "Av. Paulista",
            CityIbgeCode = "3550308"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tourist-spots", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
    }

    [Fact(DisplayName = "Deve retornar erro ao criar com cidade inexistente")]
    public async Task Create_Should_Return_BadRequest_When_City_Not_Found()
    {
        // Arrange
        var request = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu",
            Location = "Av. Paulista",
            CityIbgeCode = "9999999"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/tourist-spots", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "Deve buscar um ponto turístico por identificador")]
    public async Task GetById_Should_Return_TouristSpot()
    {
        // Arrange
        var createRequest = new TouristSpotRequest
        {
            Name = "Cristo Redentor",
            Description = "Monumento",
            Location = "Rio de Janeiro",
            CityIbgeCode = "3304557"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/tourist-spots", createRequest);

        var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponse>();

        // Act
        var response = await _client.GetAsync($"/api/tourist-spots/{created!.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var touristSpot = await response.Content.ReadFromJsonAsync<TouristSpotResponse>();

        touristSpot.Should().NotBeNull();
        touristSpot!.Name.Should().Be("Cristo Redentor");
    }

    [Fact(DisplayName = "Deve retornar não encontrado quando o ponto turístico não existir")]
    public async Task GetById_Should_Return_NotFound()
    {
        // Act
        var response = await _client.GetAsync($"/api/tourist-spots/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "Deve atualizar um ponto turístico")]
    public async Task Update_Should_Return_NoContent()
    {
        // Arrange
        var createRequest = new TouristSpotRequest
        {
            Name = "MASP",
            Description = "Museu",
            Location = "Av. Paulista",
            CityIbgeCode = "3550308"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/tourist-spots", createRequest);

        var created = await createResponse.Content.ReadFromJsonAsync<CreatedResponse>();

        var updateRequest = new TouristSpotRequest
        {
            Name = "MASP Atualizado",
            Description = "Museu Atualizado",
            Location = "Paulista Atualizada",
            CityIbgeCode = "3550308"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/tourist-spots/{created!.Id}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact(DisplayName = "Deve retornar erro ao atualizar um ponto turístico inexistente")]
    public async Task Update_Should_Return_BadRequest_When_TouristSpot_Not_Found()
    {
        // Arrange
        var request = new TouristSpotRequest
        {
            Name = "Teste",
            Description = "Teste",
            Location = "Teste",
            CityIbgeCode = "3550308"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/tourist-spots/{Guid.NewGuid()}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "Deve listar pontos turísticos paginados")]
    public async Task GetAll_Should_Return_Paged_Result()
    {
        // Act
        var response = await _client.GetAsync("/api/tourist-spots?page=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadAsStringAsync();

        result.Should().NotBeNullOrEmpty();
    }

    [Fact(DisplayName = "Deve filtrar pontos turísticos pelo termo pesquisado")]
    public async Task GetAll_Should_Filter_By_Search()
    {
        // Act
        var response = await _client.GetAsync("/api/tourist-spots?search=MASP");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}