using weatherApp.Data;
using weatherApp.Models.User;

namespace weatherApp.Services.UserService;

public class UserService : IUserService
{
    private readonly WeatherAppContext _context;

    public UserService(WeatherAppContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User> CreateUserAsync(User newUser)
    {
        var existingUser = await _context.Users.FindAsync(newUser.UserId);
        if (existingUser != null) throw new InvalidOperationException("User already exists.");

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }
}