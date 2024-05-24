using weatherApp.Data.Repositories.FollowedCityRepository;
using weatherApp.DTOs.WeatherForecastDto;
using weatherApp.Models.FollowedCity;
using weatherApp.Services.CityService;
using weatherApp.Services.weatherService;

namespace weatherApp.Services.FollowedCityService;

public class FollowedCityService : IFollowedCityService
{
    private readonly ICityService _cityService;
    private readonly IFollowedCityRepository _repository;
    private readonly IWeatherService _weatherService;


    public FollowedCityService(IFollowedCityRepository repository, IWeatherService weatherService,
        ICityService cityService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        _cityService = cityService ?? throw new ArgumentNullException(nameof(cityService));
    }

    public async Task<List<FollowedCityDTO>> GetFollowedCitiesAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));
        var followedCities = await _repository.GetFollowedCitiesAsync(userId);
        if (followedCities == null || followedCities.Count == 0) return new List<FollowedCityDTO>();

        var followedCityDTOs = new List<FollowedCityDTO>();


        foreach (var city in followedCities)
        {
            var weatherData = await _weatherService.GetWeatherByCityId(city.CityId);
            followedCityDTOs.Add(new FollowedCityDTO
            {
                CityId = city.CityId,
                Main = weatherData.Main,
                City = weatherData.City,
                WeatherDescriptions = weatherData.WeatherDescriptions
            });
        }

        return followedCityDTOs;
    }

    public async Task RemoveFollowedCityAsync(Guid userId, Guid cityId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        if (cityId == Guid.Empty)
            throw new ArgumentNullException(nameof(cityId));

        await _repository.RemoveFollowedCityAsync(userId, cityId);
    }


    public async Task AddFollowedCityAsync(Guid userId, Guid city)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        if (city == Guid.Empty)
            throw new ArgumentNullException(nameof(city));

        var followedCity = new FollowedCity
        {
            UserId = userId,
            CityId = city
        };

        await _repository.AddFollowedCityAsync(followedCity);
    }
}