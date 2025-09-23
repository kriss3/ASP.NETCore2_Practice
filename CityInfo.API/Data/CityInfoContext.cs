using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CityInfo.API.Data;

public class CityInfoContext(DbContextOptions<CityInfoContext> options) : object(options)
{
	public DbSet<City> Cities => Set<City>();
	public DbSet<PointOfInterest> PointsOfInterest => Set<PointOfInterest>();
}
