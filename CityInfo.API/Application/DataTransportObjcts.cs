namespace CityInfo.API.Application;

public record CitySummeryDto(int Id, string Name, string? Description);

public record CityDto(int Id, string Name, string? Description, IReadOnlyList<PointOfInterestDto> PointsOfInterest);

public record PointOfInterestDto(int Id, string Name, string? Description);

public record CreatePointOfInterestDto(string Name, string? Description);

public record UpdatePointOfInterestDto(string Name, string? Description);

