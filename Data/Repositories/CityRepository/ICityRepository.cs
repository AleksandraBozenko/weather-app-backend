using weatherApp.Models.City;

namespace weatherApp.Data.Repositories.CityRepository;

public interface ICityRepository
{
    Task<List<City>> GetCitiesBySearchPhrase(string searchPhrase, int skip, int take);
    Task<City> GetCityById(Guid cityId);
    Task<City> CreateCity(City city);
}