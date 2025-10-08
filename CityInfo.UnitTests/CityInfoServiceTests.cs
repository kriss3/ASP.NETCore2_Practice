using CityInfo.API.Application;
using CityInfo.API.Entities;
using FakeItEasy;

namespace CityInfo.UnitTests;

public class CityInfoServiceTests 
{
	[Fact]
	public async Task GetCity_Return_Null_When_City_Not_Found()
	{
		// Arrange
		var repo = A.Fake<ICityInfoRepository>();
		var service = new CityInfoService(repo);

		A.CallTo(() => repo.GetCityAsync(999, false, A<CancellationToken>._))
			.Returns((City?)null);

		// Act
		var result = await service.GetCityAsync(999, false, CancellationToken.None);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async Task GetCity_Returns_CityDto_When_City_Found()
	{
		// Arrange
		var repo = A.Fake<ICityInfoRepository>();
		var service = new CityInfoService(repo);

		var city = new City
		{
			Id = 1,
			Name = "Paris",
			Description = "City of Light",
			PointsOfInterest = []
		};

		A.CallTo(() => repo.GetCityAsync(1, false, A<CancellationToken>._))
			.Returns(city);

		// Act
		var result = await service.GetCityAsync(1, false, CancellationToken.None);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("Paris", result.Name);
	}
}