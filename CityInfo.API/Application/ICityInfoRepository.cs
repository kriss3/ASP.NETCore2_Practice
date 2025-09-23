using CityInfo.API.Entities;

namespace CityInfo.API.Application;

public interface ICityInfoRepository
{
	Task<IReadOnlyList<City>> GetCitiesAsync(CancellationToken cancellationToken);
	Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest, CancellationToken cancellationToken);

	Task<PointOfInterest?> GetPointAsync(int cityId, int pointId, CancellationToken cancellationToken);
	Task AddPointAsync(City city, PointOfInterest point, CancellationToken ccancellationTokent);
	void DeletePoint(PointOfInterest point);

	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
