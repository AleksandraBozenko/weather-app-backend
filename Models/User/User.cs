using System.ComponentModel.DataAnnotations;
using weatherApp.Models.City;

namespace weatherApp.Models.User;

public class User
{
    [Key] public string UserId { get; set; }

    public string Email { get; set; }
    public string Name { get; set; }
    public FavouriteCity FavouriteCity { get; set; }
    public List<FollowedCity.FollowedCity> FollowedCities { get; set; }
}