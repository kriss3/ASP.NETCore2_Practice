
using CityInfo.API.Entities;

namespace CityInfo.API.Application;

public class CityInfoService(ICityInfoRepository repo) : ICityInfoService
{
	private readonly ICityInfoRepository _repo = repo;

	public async Task<IReadOnlyList<CitySummaryDto>> GetCitiesAsync(CancellationToken cancellationToken)
	{
		


	}


	public async Task<PointOfInterestDto?> AddPointOfInterestAsync(int cityId, CreatePointOfInterestDto input, CancellationToken cancellationToken)
	{
		
	}

	public Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId, CancellationToken cancellationToken)
	{
		
	}

	

	public Task<CityDto?> GetCityAsync(int cityId, bool includePointsOfInterest, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, UpdatePointOfInterestDto input, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
