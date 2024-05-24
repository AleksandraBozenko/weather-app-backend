using weatherApp.Models.User;

public interface IUserRepository
{
    Task<User> GetUserAsync(string userId);
    Task<User> AddUserAsync(User user);
}