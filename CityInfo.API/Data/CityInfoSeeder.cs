using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data;

public static class CityInfoSeeder
{
	public static async Task SeedAsync(CityInfoContext db) 
	{
		if (await db.Cities.AnyAsync()) 
			return;
	}
}
