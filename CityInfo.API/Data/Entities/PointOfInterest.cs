namespace CityInfo.API.Data.Entities;

public class PointOfInterest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
