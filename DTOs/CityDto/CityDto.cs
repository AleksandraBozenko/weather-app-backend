namespace weatherApp.DTOs.CityDto;

public class CityDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}