using weatherApp.Models.FollowedCity;

namespace weatherApp.Data.Repositories.FollowedCityRepository;

public interface IFollowedCityRepository
{
    Task<List<FollowedCity>> GetFollowedCitiesAsync(Guid userId);
    Task AddFollowedCityAsync(FollowedCity followedCity);
    Task RemoveFollowedCityAsync(Guid userId, Guid cityId);
}