using Microsoft.EntityFrameworkCore;
using weatherApp.Models.City;
using weatherApp.Models.FollowedCity;
using weatherApp.Models.User;

namespace weatherApp.Data;

public class WeatherAppContext : DbContext
{
    public WeatherAppContext(DbContextOptions<WeatherAppContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FavouriteCity> FavouriteCities { get; set; }
    public DbSet<FollowedCity> FollowedCities { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.Name).IsRequired();
        });

        modelBuilder.Entity<FavouriteCity>(entity =>
        {
            entity.Property(c => c.UserId).IsRequired();
            entity.Property(c => c.CityId).IsRequired();
        });

        modelBuilder.Entity<FollowedCity>(entity =>
        {
            entity.Property(fc => fc.UserId).IsRequired();
            entity.Property(fc => fc.CityId).IsRequired();
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired();
            entity.Property(c => c.Country).IsRequired();
            entity.Property(c => c.Latitude).IsRequired();
            entity.Property(c => c.Longitude).IsRequired();
        });
    }
}