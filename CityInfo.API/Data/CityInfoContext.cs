using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data;

// Fix: Inherit from DbContext, and call base(options) in the constructor.
public class CityInfoContext(DbContextOptions<CityInfoContext> options) : DbContext(options)
{
	public DbSet<City> Cities => Set<City>();
    public DbSet<PointOfInterest> PointsOfInterest => Set<PointOfInterest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
