using CityInfo.API.Data.Entities;

namespace CityInfo.API.Application;

public static class ManualMappers
{
	public static CityDto ToDto(this City c) =>
		new(c.Id, c.Name, c.Description,
			[.. c.PointsOfInterest.Select(p => p.ToDto())]);

	public static PointOfInterestDto ToDto(this PointOfInterest p) =>
		new(p.Id, p.Name, p.Description);

	public static void Apply(this UpdatePointOfInterestDto dto, PointOfInterest entity)
	{
		entity.Name = dto.Name;
		entity.Description = dto.Description ?? string.Empty;
	}
}
