using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace weatherApp.DTOs.WeatherForecastDto
{
    public class ForecastInterval
    {
        [JsonPropertyName("dt")] public long DateTimestamp { get; set; }
        [JsonPropertyName("main")] public IntervalMainWeatherData Main { get; set; }
        [JsonPropertyName("weather")] public List<WeatherDescription> WeatherDescriptions { get; set; }
        [JsonPropertyName("clouds")] public CloudsData Clouds { get; set; }
        [JsonPropertyName("wind")] public WindDataList Wind { get; set; }
        [JsonPropertyName("visibility")] public int Visibility { get; set; }
        [JsonPropertyName("pop")] public double ProbabilityOfPrecipitation { get; set; }
        [JsonPropertyName("rain")] public RainData Rain { get; set; }
        [JsonPropertyName("dt_txt")] public string DateTimeText { get; set; }

        public DateTime Date
        {
            get { return DateTimeOffset.FromUnixTimeSeconds(DateTimestamp).DateTime; }
        }
    }

    public class IntervalMainWeatherData
    {
        [JsonPropertyName("temp")] public double Temperature { get; set; }
        [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }
        [JsonPropertyName("temp_min")] public double TempMin { get; set; }
        [JsonPropertyName("temp_max")] public double TempMax { get; set; }
        [JsonPropertyName("pressure")] public double Pressure { get; set; }
        [JsonPropertyName("sea_level")] public double SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")] public double GroundLevel { get; set; }
        [JsonPropertyName("humidity")] public int Humidity { get; set; }
        [JsonPropertyName("temp_kf")] public double TempKf { get; set; }
    }

    public class CloudsData
    {
        [JsonPropertyName("all")] public int All { get; set; }
    }

    public class WindDataList
    {
        [JsonPropertyName("speed")] public double Speed { get; set; }
        [JsonPropertyName("deg")] public int Degree { get; set; }
        [JsonPropertyName("gust")] public double Gust { get; set; }
    }

    public class RainData
    {
        [JsonPropertyName("3h")] public double VolumeForLast3Hours { get; set; }
    }

    public class WeatherForecastListDTO
    {
        [JsonPropertyName("city")] public CityInfo City { get; set; }
        [JsonPropertyName("cnt")] public int Count { get; set; }
        [JsonPropertyName("list")] public List<ForecastInterval> Intervals { get; set; }
    }

    public class CityInfo
    {
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}
