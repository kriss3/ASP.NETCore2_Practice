namespace CityInfo.API.Application;

public interface ICityInfoService
{
	Task<IReadOnlyList<CitySummaryDto>> GetCitiesAsync(CancellationToken cancellationToken);
	Task<CityDto?> GetCityAsync(int cityId, bool includePointsOfInterest, CancellationToken cancellationToken);
	Task<CityDto?> AddCityAsync(CreateCityDto input, CancellationToken cancellationToken);


	// Points of Interest CRUD ops.
	Task<PointOfInterestDto?> AddPointOfInterestAsync(int cityId, CreatePointOfInterestDto input, CancellationToken cancellationToken);
	Task<bool> UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, UpdatePointOfInterestDto input, CancellationToken cancellationToken);
	Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId, CancellationToken cancellationToken);

}
