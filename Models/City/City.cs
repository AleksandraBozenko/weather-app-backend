namespace weatherApp.Models.City;

public class City
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}