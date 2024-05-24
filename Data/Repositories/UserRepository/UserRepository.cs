using Microsoft.EntityFrameworkCore;
using weatherApp.Models.User;

namespace weatherApp.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WeatherAppContext _context;

    public UserRepository(WeatherAppContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserAsync(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
}