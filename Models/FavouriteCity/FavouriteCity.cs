namespace weatherApp.Models.City;

public class FavouriteCity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; }
    public string CityId { get; set; }
}