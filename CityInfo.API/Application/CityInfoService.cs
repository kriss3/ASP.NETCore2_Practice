
namespace CityInfo.API.Application;

public class CityInfoService : ICityInfoService
{
	private readonly ICityRepository _repo;
	public CityInfoService(ICityRepository repo) => _repo = repo;

	public async Task<PointOfInterestDto?> AddPointOfInterestAsync(int cityId, CreatePointOfInterestDto input, CancellationToken cancellationToken)
	{
		return await _repo.GetCitiesAsync(ct)).Select(c => c.ToSummaryDto()).ToList();

	}

	public Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId, CancellationToken cancellationToken)
	{
		
	}

	public Task<IReadOnlyList<CitySummaryDto>> GetCitiesAsync(CancellationToken cancellationToken)
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
