
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Application;

public class CityInfoService(ICityInfoRepository repo) : ICityInfoService
{
	private readonly ICityInfoRepository _repo = repo;

	public async Task<IReadOnlyList<CitySummaryDto>> GetCitiesAsync(CancellationToken cancellationToken)
	{
		var cities = await _repo.GetCitiesAsync(cancellationToken);
		// map entities -> summary DTOs
		return [.. cities.Select(c => c.ToSummaryDto())];
	}

	public async Task<CityDto?> GetCityAsync(int cityId, bool includePointsOfInterest, CancellationToken cancellationToken)
	{


	}

	public async Task<PointOfInterestDto?>  AddPointOfInterestAsync(int cityId, CreatePointOfInterestDto input, CancellationToken cancellationToken)
	{


	}

	public Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId, CancellationToken cancellationToken)
	{
		
	}

	


	public Task<bool> UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, UpdatePointOfInterestDto input, CancellationToken cancellationToken)
	{
		
	}
}
