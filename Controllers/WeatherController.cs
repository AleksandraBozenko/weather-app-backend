using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using weatherApp.Services.weatherService;

namespace weatherApp.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetWeatherByCity(Guid cityId)
    {
        Console.WriteLine("AllDataFromJwt");
        Console.WriteLine(User);
        try
        {
            var weather = await _weatherService.GetWeatherByCityId(cityId);
            return Ok(weather);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, ex.Message);
        }
        catch (JsonException)
        {
            return BadRequest("Failed to deserialize the weather data.");
        }
    }

    [HttpGet("forecast/{cityId}")]
    public async Task<IActionResult> GetForecastByCity(Guid cityId)
    {
        try
        {
            var forecast = await _weatherService.GetForecastByCityId(cityId);
            return Ok(forecast);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, ex.Message);
        }
        catch (JsonException)
        {
            return BadRequest("Failed to deserialize the forecast data.");
        }
    }
}