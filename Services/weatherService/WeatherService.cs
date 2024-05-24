using System.Text.Json;
using Microsoft.Extensions.Options;
using weatherApp.DTOs.WeatherForecastDto;
using weatherApp.Services.CityService;
using weatherApp.Settings;

namespace weatherApp.Services.weatherService;

public class WeatherService : IWeatherService
{
    private readonly ICityService _cityService;
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherSettings _settings;


    public WeatherService(HttpClient httpClient, IOptions<OpenWeatherSettings> options, ICityService cityService)
    {
        _httpClient = httpClient;
        _settings = options.Value;
        _cityService = cityService;
    }

    public async Task<WeatherForecastDTO> GetWeatherByCityId(Guid cityId)
    {
        var city = await _cityService.GetCityById(cityId);
        var response = await _httpClient.GetAsync(
            $"{_settings.BaseUrl}weather?lat={city.Latitude}&lon={city.Longitude}&appid={_settings.ApiKey}&units=metric&lang=pl");


        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var weather = JsonSerializer.Deserialize<WeatherForecastDTO>(content);


            if (weather != null)
                return weather;
            throw new JsonException("Failed to deserialize the weather data.");
        }

        throw new HttpRequestException($"Error fetching weather data from OpenWeather API: {response.ReasonPhrase}");
    }


    public async Task<WeatherForecastListDTO> GetForecastByCityId(Guid cityId)
    {
        var city = await _cityService.GetCityById(cityId);
        var response = await _httpClient.GetAsync(
            $"{_settings.BaseUrl}forecast?lat={city.Latitude}&lon={city.Longitude}&appid={_settings.ApiKey}&units=metric&lang=pl"
        );

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var forecast = JsonSerializer.Deserialize<WeatherForecastListDTO>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (forecast != null)
            {
                var filteredIntervals = forecast.Intervals.Where(i => i.Date.Hour == 12).ToList();
                forecast.Intervals = filteredIntervals;
                return forecast;
            }

            throw new JsonException("Failed to deserialize the forecast data.");
        }

        throw new HttpRequestException($"Error fetching forecast data from OpenWeather API: {response.ReasonPhrase}");
    }
}