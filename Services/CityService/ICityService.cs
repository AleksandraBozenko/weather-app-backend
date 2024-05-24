using weatherApp.DTOs.CityDto;

namespace weatherApp.Services.CityService;

public interface ICityService
{
    Task<List<CityDTO>> GetCitiesBySearchPhrase(string searchPhrase, int skip, int take);
    Task<CityDTO> GetCityById(Guid cityId);
    Task<CityDTO> CreateCity(CityDTO city);
}