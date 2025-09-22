namespace CityInfo.API.Entities;

public class City
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public int NumberOfPointsOfInterest
    {
        get { return PointsOfInterest.Count; }
    }

    public ICollection<PointOfInterest> PointsOfInterest { get; set; } = [];
}
