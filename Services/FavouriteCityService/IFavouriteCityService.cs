using weatherApp.DTOs.FavouriteCityDTO;

public interface IFavouriteCityService
{
    Task<FavouriteCityDTO> GetFavouriteCityAsync(string userId);
    Task AddOrUpdateFavouriteCityAsync(string userId, FavouriteCityDTO cityDto);
    Task RemoveFavouriteCityAsync(string userId);
}