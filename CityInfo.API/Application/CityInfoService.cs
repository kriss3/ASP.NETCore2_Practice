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

	public async Task<CityDto?> GetCityAsync(
		int cityId, 
		bool includePointsOfInterest, 
		CancellationToken cancellationToken)
	{
		var city = await _repo.GetCityAsync(cityId, includePointsOfInterest, cancellationToken);
		return city?.ToDto();
	}

	public async Task<PointOfInterestDto?>  AddPointOfInterestAsync(
		int cityId, 
		CreatePointOfInterestDto input, 
		CancellationToken cancellationToken)
	{
		//to add first attempt to get it:
		var city = await _repo.GetCityAsync(cityId, includePointsOfInterest: true, cancellationToken);
		if (city is null) 
			return null;

		var poi = input.ToEntity(); // get POI in a form of DTO
		await _repo.AddPointAsync(city, poi, cancellationToken);
		await _repo.SaveChangesAsync(cancellationToken);
		return poi.ToDto();
	}
	public async Task<bool> UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, UpdatePointOfInterestDto input, CancellationToken cancellationToken)
	{
		var poi = await _repo.GetPointAsync(cityId, pointOfInterestId, cancellationToken);
		if (poi is null) 
			return false;

		input.Apply(poi);
		await _repo.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> DeletePointOfInterestAsync(int cityId, int pointOfInterestId, CancellationToken cancellationToken)
	{
		
	}
}
