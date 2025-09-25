using CityInfo.API.Application;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Data;

public class CityInfoRepository(CityInfoContext context) : ICityInfoRepository
{
	private readonly CityInfoContext _context = context;

	public async Task<IReadOnlyList<City>> GetCitiesAsync(CancellationToken cancellationToken)
	{
		var result = await _context.Cities
			.OrderBy(c => c.Name)
			.ToListAsync(cancellationToken);
		return result;
	}

	public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest, CancellationToken cancellationToken)
	{
		if (includePointsOfInterest)
		{
			return await _context.Cities
				.Include(c => c.PointsOfInterest)
				.FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
		}
		return await _context.Cities
			.FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);
	}

	public Task<PointOfInterest?> GetPointAsync(int cityId, int pointId, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task AddPointAsync(City city, PointOfInterest point, CancellationToken ccancellationTokent)
	{
		throw new NotImplementedException();
	}

	public void DeletePoint(PointOfInterest point)
	{
		throw new NotImplementedException();
	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
