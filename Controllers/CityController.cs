using Microsoft.AspNetCore.Mvc;
using weatherApp.DTOs.CityDto;
using weatherApp.Services.CityService;

namespace YourProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities([FromQuery] string cityName, [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        if (string.IsNullOrEmpty(cityName)) return BadRequest("City name cannot be null or empty.");

        var cities = await _cityService.GetCitiesBySearchPhrase(cityName, skip, take);
        return Ok(cities);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CityDTO city)
    {
        var createdCity = await _cityService.CreateCity(city);
        return CreatedAtAction(nameof(_cityService.GetCityById), new { cityId = createdCity.Id }, createdCity);
    }
}