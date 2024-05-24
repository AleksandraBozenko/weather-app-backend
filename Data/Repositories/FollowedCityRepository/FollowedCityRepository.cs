using Microsoft.EntityFrameworkCore;
using weatherApp.Models.FollowedCity;

namespace weatherApp.Data.Repositories.FollowedCityRepository;

public class FollowedCityRepository : IFollowedCityRepository
{
    private readonly WeatherAppContext _context;

    public FollowedCityRepository(WeatherAppContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddFollowedCityAsync(FollowedCity followedCity)
    {
        if (followedCity == null)
            throw new ArgumentNullException(nameof(followedCity));

        _context.FollowedCities.Add(followedCity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFollowedCityAsync(Guid userId, Guid cityId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        if (cityId == Guid.Empty)
            throw new ArgumentNullException(nameof(cityId));

        var followedCity =
            await _context.FollowedCities.FirstOrDefaultAsync(fc => fc.UserId == userId && fc.CityId == cityId);

        if (followedCity != null)
        {
            _context.FollowedCities.Remove(followedCity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<FollowedCity>> GetFollowedCitiesAsync(Guid userId)
    {
        return await _context.FollowedCities.Where(fc => fc.UserId == userId).ToListAsync();
    }
}