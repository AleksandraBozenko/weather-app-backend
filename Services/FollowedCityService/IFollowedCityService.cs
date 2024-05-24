using weatherApp.DTOs.WeatherForecastDto;

namespace weatherApp.Services.FollowedCityService;

public interface IFollowedCityService
{
    Task<List<FollowedCityDTO>> GetFollowedCitiesAsync(Guid userId);
    Task AddFollowedCityAsync(Guid userId, Guid city);
    Task RemoveFollowedCityAsync(Guid userId, Guid cityId);
}