using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data;

public static class CityInfoSeeder
{
	public static async Task SeedAsync(CityInfoContext db) 
	{
		if (await db.Cities.AnyAsync()) 
			return;

		var vancouver = new City { Name = "Vancouver", Description = "Rainy but lovely." };
		vancouver.PointsOfInterest.Add(new PointOfInterest { Name = "Stanley Park", Description = "Huge urban park." });
		vancouver.PointsOfInterest.Add(new PointOfInterest { Name = "Granville Island", Description = "Public market." });

		var calgary = new City { Name = "Calgary", Description = "Stampede city." };
		calgary.PointsOfInterest.Add(new PointOfInterest { Name = "Calgary Tower", Description = "Observation tower." });

		db.Cities.AddRange(vancouver, calgary);
		await db.SaveChangesAsync();
	}
}
