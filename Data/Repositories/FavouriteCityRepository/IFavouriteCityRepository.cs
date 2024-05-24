using weatherApp.Models.City;

public interface IFavouriteCityRepository
{
    Task<List<FavouriteCity>> GetFavouriteCitiesAsync(string userId);
    Task AddOrUpdateFavouriteCityAsync(FavouriteCity city);
    Task RemoveFavouriteCityAsync(string userId);
}