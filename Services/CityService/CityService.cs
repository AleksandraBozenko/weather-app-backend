using weatherApp.Data.Repositories.CityRepository;
using weatherApp.DTOs.CityDto;
using weatherApp.Models.City;

namespace weatherApp.Services.CityService;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }


    public async Task<List<CityDTO>> GetCitiesBySearchPhrase(string searchPhrase, int skip, int take)
    {
        var cities = await _cityRepository.GetCitiesBySearchPhrase(searchPhrase, skip, take);
        if (cities == null || cities.Count == 0) return new List<CityDTO>();

        var cityDTOs = cities.Select(city => new CityDTO
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            Latitude = city.Latitude,
            Longitude = city.Longitude
        }).ToList();

        return cityDTOs;
    }

    public async Task<CityDTO> GetCityById(Guid cityId)
    {
        var city = await _cityRepository.GetCityById(cityId);
        if (city == null) return null;

        return new CityDTO
        {
            Name = city.Name,
            Country = city.Country,
            Latitude = city.Latitude,
            Longitude = city.Longitude
        };
    }

    public async Task<CityDTO> CreateCity(CityDTO city)
    {
        var cityModel = new City
        {
            Name = city.Name,
            Country = city.Country,
            Latitude = city.Latitude,
            Longitude = city.Longitude
        };

        var createdCity = await _cityRepository.CreateCity(cityModel);

        return new CityDTO
        {
            Id = createdCity.Id,
            Name = createdCity.Name,
            Country = createdCity.Country,
            Latitude = createdCity.Latitude,
            Longitude = createdCity.Longitude
        };
    }
}