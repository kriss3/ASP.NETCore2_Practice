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

	public async Task<City> AddCityAsync(City city, CancellationToken cancellationToken) 
	{
		var result = await _context.Cities.AddAsync(city, cancellationToken);
		return result.Entity;
	}

	public async Task<PointOfInterest?> GetPointAsync(int cityId, int pointId, CancellationToken cancellationToken)
	{
		var result = await _context.PointsOfInterest
			.FirstOrDefaultAsync(p => p.Id == cityId && p.Id == pointId, cancellationToken);
		return result;
	}

	public async Task AddPointAsync(City city, PointOfInterest point, CancellationToken ccancellationTokent)
	{
		city.PointsOfInterest.Add(point);
		await Task.CompletedTask;
	}

	public async Task DeletePoint(PointOfInterest point)
	{
		_context.PointsOfInterest.Add(point);
		await Task.CompletedTask;

	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
	{
		var result = await _context.SaveChangesAsync(cancellationToken);
		return result;
	}
}
