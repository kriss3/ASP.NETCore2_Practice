using CityInfo.API.Data.Entities;

namespace CityInfo.API.Application;

public static class ManualMappers
{

	public static PointOfInterestDto ToDto(this PointOfInterest p) =>
		new(p.Id, p.Name, p.Description);

	public static void Apply(this UpdatePointOfInterestDto dto, PointOfInterest entity)
	{
		entity.Name = dto.Name;
		entity.Description = dto.Description ?? string.Empty;
	}
}
