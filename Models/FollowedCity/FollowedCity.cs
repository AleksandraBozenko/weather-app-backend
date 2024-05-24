namespace weatherApp.Models.FollowedCity;

public class FollowedCity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid CityId { get; set; }
}