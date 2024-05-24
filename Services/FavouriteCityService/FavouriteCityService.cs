using AutoMapper;
using weatherApp.DTOs.FavouriteCityDTO;
using weatherApp.Models.City;
using weatherApp.Services.UserService;

namespace weatherApp.Services.FavouriteCityService;

public class FavouriteCityService : IFavouriteCityService
{
    private readonly IMapper _mapper;
    private readonly IFavouriteCityRepository _repository;
    private readonly IUserService _userService;

    public FavouriteCityService(IFavouriteCityRepository repository, IMapper mapper, IUserService userService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task AddOrUpdateFavouriteCityAsync(string userId, FavouriteCityDTO city)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        var user = await _userService.GetUserAsync(userId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        if (city == null)
            throw new ArgumentNullException(nameof(city));

        var favouriteCity = _mapper.Map<FavouriteCity>(city);
        favouriteCity.UserId = userId;

        await _repository.AddOrUpdateFavouriteCityAsync(favouriteCity);
    }

    public async Task RemoveFavouriteCityAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        await _repository.RemoveFavouriteCityAsync(userId);
    }

    public async Task<FavouriteCityDTO> GetFavouriteCityAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(nameof(userId));

        var user = await _userService.GetUserAsync(userId);
        if (user == null)
            throw new InvalidOperationException("User not found.");

        var favouriteCities = await _repository.GetFavouriteCitiesAsync(userId);
        var firstFavouriteCity = favouriteCities.FirstOrDefault();
        if (firstFavouriteCity == null)

            throw new InvalidOperationException("Favourite city not found.");

        return _mapper.Map<FavouriteCityDTO>(firstFavouriteCity);
    }
}