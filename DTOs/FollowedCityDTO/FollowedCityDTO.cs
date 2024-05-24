using weatherApp.DTOs.WeatherForecastDto;

namespace weatherApp.DTOs.FollowedCityDTO;

public class FollowedCityDTO
{
    public Guid CityId { get; set; }
    public string CityName { get; set; }

    public MainWeatherData WeatherData { get; set; }
}