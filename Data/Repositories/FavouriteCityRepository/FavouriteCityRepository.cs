using Microsoft.EntityFrameworkCore;
using weatherApp.Data;
using weatherApp.Models.City;

public class FavouriteCityRepository : IFavouriteCityRepository
{
    private readonly WeatherAppContext _context;

    public FavouriteCityRepository(WeatherAppContext context)
    {
        _context = context;
    }

    public async Task<List<FavouriteCity>> GetFavouriteCitiesAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        return await _context.FavouriteCities
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task AddOrUpdateFavouriteCityAsync(FavouriteCity city)
    {
        var existingCity = await _context.FavouriteCities.FirstOrDefaultAsync(c =>
            c.UserId == city.UserId);

        if (existingCity != null) _context.FavouriteCities.Remove(existingCity);

        _context.FavouriteCities.Add(city);
        await _context.SaveChangesAsync();
    }


    public async Task RemoveFavouriteCityAsync(string userId)
    {
        var cities = await _context.FavouriteCities
            .Where(c => c.UserId == userId)
            .ToListAsync();

        _context.FavouriteCities.RemoveRange(cities);
        await _context.SaveChangesAsync();
    }
}