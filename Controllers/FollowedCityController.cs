using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using weatherApp.DTOs.FollowedCityDTO;
using weatherApp.Models.User;
using weatherApp.Services.FollowedCityService;
using weatherApp.Services.UserService;

namespace weatherApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowedCityController : ControllerBase
{
    private readonly IFollowedCityService _followedCityService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public FollowedCityController(IFollowedCityService followedCityService, IUserService userService,
        IHttpContextAccessor httpContextAccessor)
    {
        _followedCityService = followedCityService;
        _httpContextAccessor = httpContextAccessor;

        _userService = userService;
    }

    private string GetUserIdFromToken()
    {
        return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddFollowedCity(Guid CityId)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var userIdGuid = Guid.Parse(userId);

            if (userIdGuid == Guid.Empty) return BadRequest("Invalid user id");

            await AddUserIfNotExists();
            await _followedCityService.AddFollowedCityAsync(userIdGuid, CityId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> RemoveFollowedCity(Guid cityId)
    {
        try
        {
            var userId = GetUserIdFromToken();
            var userIdGuid = Guid.Parse(userId);
            if (userIdGuid == Guid.Empty) return BadRequest("Invalid user id");
            await _followedCityService.RemoveFollowedCityAsync(userIdGuid, cityId);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FollowedCityDTO>> GetFollowedCity()
    {
        var userId = GetUserIdFromToken();
        await AddUserIfNotExists();
        var userIdGuid = Guid.Parse(userId);

        if (userIdGuid == Guid.Empty) return BadRequest("Invalid user id");

        var followedCities = await _followedCityService.GetFollowedCitiesAsync(userIdGuid);
        return Ok(followedCities);
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