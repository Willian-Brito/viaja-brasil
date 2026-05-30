using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ViajaBrasil.Tests.Integration.Configuration;

namespace ViajaBrasil.Tests.Integration.Controllers;

public class StatesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StatesControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "Deve retornar todos os estados")]
    public async Task Get_Should_Return_All_States()
    {
        // Act
        var response = await _client.GetAsync("/api/states");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<string[]>();

        content.Should().NotBeNull();
        content.Should().Contain("SP");
        content.Should().Contain("RJ");
        content.Should().Contain("MG");
    }

    [Fact(DisplayName = "Deve retornar cidades de um estado válido")]
    public async Task GetCities_Should_Return_Cities()
    {
        // Act
        var response = await _client.GetAsync("/api/states/SP/cities");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrEmpty();
    }

    [Fact(DisplayName = "Deve retornar erro quando o estado for inválido")]
    public async Task GetCities_Should_Return_BadRequest_When_State_Is_Invalid()
    {
        // Act
        var response = await _client.GetAsync("/api/states/XX/cities");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}