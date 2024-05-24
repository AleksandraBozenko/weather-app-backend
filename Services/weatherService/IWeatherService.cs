using weatherApp.DTOs.WeatherForecastDto;

namespace weatherApp.Services.weatherService;

public interface IWeatherService
{
    Task<WeatherForecastDTO> GetWeatherByCityId(Guid cityId);
    Task<WeatherForecastListDTO> GetForecastByCityId(Guid cityId);
}