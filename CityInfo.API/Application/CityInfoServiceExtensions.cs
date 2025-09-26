using CityInfo.API.Entities;

namespace CityInfo.API.Application;

public static class CityInfoServiceExtensions
{
	public static CitySummaryDto ToSummaryDto(this City c) 
	{
		return new(c.Id, c.Name, c.Description);
	}

	public static CityDto ToDto(this City c) 
	{
		return new(c.Id, c.Name, c.Description,
			[.. (c.PointsOfInterest ?? []).Select(p => p.ToDto())]);
	}

	public static PointOfInterestDto ToDto(this PointOfInterest p) 
	{
		return new(p.Id, p.Name, p.Description);
	}

	public static PointOfInterest ToEntity(this CreatePointOfInterestDto dto) 
	{
		return new() { Name = dto.Name, Description = dto.Description };
	}

	public static void Apply(this UpdatePointOfInterestDto dto, PointOfInterest entity) 
	{
		entity.Name = dto.Name;
		entity.Description = dto.Description;
	}
}

