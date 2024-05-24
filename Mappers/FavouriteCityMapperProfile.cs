using AutoMapper;
using weatherApp.DTOs.FavouriteCityDTO;
using weatherApp.Models.City;

namespace weatherApp.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<FavouriteCity, FavouriteCityDTO>();
        CreateMap<FavouriteCityDTO, FavouriteCity>();
    }
}