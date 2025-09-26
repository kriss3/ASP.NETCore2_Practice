using CityInfo.API.Entities;

namespace CityInfo.API.Application;

public static class CityInfoServiceExtensions
{
	public static CitySummaryDto ToSummaryDto(this City c) { }

	public static CityDto ToDto(this City c) { }


	public static PointOfInterestDto ToDto(this PointOfInterest p) { }

	public static PointOfInterest ToEntity(this CreatePointOfInterestDto dto) { }

	public static void Apply(this UpdatePointOfInterestDto dto, PointOfInterest entity) { }
}

