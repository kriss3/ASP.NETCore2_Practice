using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Data;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int NumberOfPointsOfInterest
    {
        get { return PointsOfInterest.Count; }
    }

    public ICollection<PointOfInterest> PointsOfInterest { get; set; } =
        new List<PointOfInterest>();
}
