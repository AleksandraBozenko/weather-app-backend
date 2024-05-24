using weatherApp.Models.User;

namespace weatherApp.Services.UserService;

public interface IUserService
{
    Task<User> GetUserAsync(string userId);
    Task<User> CreateUserAsync(User newUser);
}