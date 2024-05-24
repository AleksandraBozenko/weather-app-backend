using System.Text.Json.Serialization;

namespace weatherApp.DTOs.WeatherForecastDto;

public class MainWeatherData
{
    [JsonPropertyName("temp")] public double Temperature { get; set; }

    [JsonPropertyName("humidity")] public double Humidity { get; set; }

    [JsonPropertyName("pressure")] public double Pressure { get; set; }

    [JsonPropertyName("temp_max")] public double MaxTemperature { get; set; }

    [JsonPropertyName("temp_min")] public double MinTemperature { get; set; }
}

public class WeatherDescription
{
    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("icon")] public string Icon { get; set; }
}

public class WindData
{
    [JsonPropertyName("speed")] public double Speed { get; set; }

    [JsonPropertyName("deg")] public double Degree { get; set; }
}

public class SysData
{
    [JsonPropertyName("sunrise")] public long Sunrise { get; set; }

    [JsonPropertyName("sunset")] public long Sunset { get; set; }
}

public class WeatherForecastDTO
{
    [JsonPropertyName("name")] public string City { get; set; }

    [JsonPropertyName("main")] public MainWeatherData Main { get; set; }

    [JsonPropertyName("weather")] public List<WeatherDescription> WeatherDescriptions { get; set; }

    [JsonPropertyName("wind")] public WindData Wind { get; set; }

    [JsonPropertyName("visibility")] public int Visibility { get; set; }

    [JsonPropertyName("sys")] public SysData Sys { get; set; }
}

public class FollowedCityDTO
{
    public Guid CityId { get; set; }
    [JsonPropertyName("name")] public string City { get; set; }
    [JsonPropertyName("main")] public MainWeatherData Main { get; set; }
    [JsonPropertyName("weather")] public List<WeatherDescription> WeatherDescriptions { get; set; }
}