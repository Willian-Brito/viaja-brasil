using Microsoft.AspNetCore.Mvc;
using ViajaBrasil.Application.DTOs.Requests;
using ViajaBrasil.Application.Interfaces;

namespace ViajaBrasil.API.Controllers;

[ApiController]
[Route("api/tourist-spots")]
public class TouristSpotsController : ControllerBase
{
    private readonly ITouristSpotService _service;

    public TouristSpotsController(ITouristSpotService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TouristSpotRequest request)
    {
        var id = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            new { id });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, TouristSpotRequest request)
    {
        await _service.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var touristSpot = await _service.GetByIdAsync(id);

        if (touristSpot is null)
            return NotFound();

        return Ok(touristSpot);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5
    )
    {
        var result = await _service.GetAllAsync(search, page, pageSize);
        return Ok(result);
    }
}