using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using weatherApp.DTOs.FavouriteCityDTO;
using weatherApp.Models.User;
using weatherApp.Services.UserService;

namespace weatherApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavouriteCityController : ControllerBase
{
    private readonly IFavouriteCityService _favouriteCityService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public FavouriteCityController(IFavouriteCityService favouriteCityService, IHttpContextAccessor httpContextAccessor,
        IUserService userService)
    {
        _favouriteCityService = favouriteCityService;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    private string GetUserIdFromToken()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FavouriteCityDTO>> GetFavouriteCity()
    {
        try
        {
            var userId = GetUserIdFromToken();
            await AddUserIfNotExists();
            var favouriteCity = await _favouriteCityService.GetFavouriteCityAsync(userId);
            if (favouriteCity == null) return NotFound("Favourite city not found.");
            return Ok(favouriteCity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddOrUpdateFavouriteCity(string CityId)
    {
        try
        {
            var userId = GetUserIdFromToken();
            await AddUserIfNotExists();
            var favouriteCity = new FavouriteCityDTO
            {
                CityId = CityId
            };

            await _favouriteCityService.AddOrUpdateFavouriteCityAsync(userId, favouriteCity);
            return Ok("Favourite city added/updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> RemoveFavouriteCity()
    {
        try
        {
            var userId = GetUserIdFromToken();
            await _favouriteCityService.RemoveFavouriteCityAsync(userId);
            return Ok("Favourite city removed successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private async Task AddUserIfNotExists()
    {
        var userId = GetUserIdFromToken();
        var user = await _userService.GetUserAsync(userId);

        if (user == null)
        {
            var email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var name = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var newUser = new User
            {
                UserId = userId,
                Email = email,
                Name = name
            };
            await _userService.CreateUserAsync(newUser);
        }
    }
}