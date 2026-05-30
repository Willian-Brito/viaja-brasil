using Microsoft.AspNetCore.Mvc;
using ViajaBrasil.Application.Interfaces;
using ViajaBrasil.Domain.Enums;

namespace ViajaBrasil.API.Controllers;

[ApiController]
[Route("api/states")]
public class StatesController : ControllerBase
{
    private readonly ICityService _cityService;

    public StatesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var states = Enum.GetNames<State>();
        return Ok(states);
    }
    
    [HttpGet("{state}/cities")]
    public async Task<IActionResult> GetCities(string state)
    {
        var cities = await _cityService.GetByStateAsync(state);
        return Ok(cities);
    }
}