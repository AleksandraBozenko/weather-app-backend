using Microsoft.EntityFrameworkCore;
using weatherApp.Data;
using weatherApp.Data.Repositories.CityRepository;
using weatherApp.Models.City;

public class CityRepository : ICityRepository
{
    private readonly WeatherAppContext _context;

    public CityRepository(WeatherAppContext context)
    {
        _context = context;
    }

    public async Task<List<City>> GetCitiesBySearchPhrase(string searchPhrase, int skip, int take)
    {
        var normalizedSearchPhrase = searchPhrase.ToLower();
        return await _context.Cities
            .Where(city => city.Name.ToLower().Contains(normalizedSearchPhrase))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<City> CreateCity(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        return city;
    }

    public async Task<City> GetCityById(Guid cityId)
    {
        return await _context.Cities
            .FirstOrDefaultAsync(city => city.Id == cityId);
    }
}