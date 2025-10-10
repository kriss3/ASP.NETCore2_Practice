using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using CityInfo.API.Application;
using CityInfo.API.Entities;
using FakeItEasy;

namespace CityInfo.UnitTests;

public class CityInfoServiceTests 
{
	private IFixture _fixture;

	public CityInfoServiceTests()
	{
		_fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
	}

	[Fact]
	public async Task GetCity_Return_Null_When_City_Not_Found()
	{


		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<ICityInfoService>();
		var nonExistantCityId = _fixture.Create<int>();

		A.CallTo(() => repo.GetCityAsync(nonExistantCityId, false, A<CancellationToken>._))
			.Returns((City?)null);

		// Act
		var result = await service.GetCityAsync(nonExistantCityId, false, CancellationToken.None);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public async Task GetCity_Returns_CityDto_When_City_Found()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();

		var city = _fixture.Build<City>()
			.With(c => c.Id, 1)
			.With(c => c.Name, "Paris")
			.With(c => c.Description, "Dirty City")
			.With(c => c.PointsOfInterest, [])
			.Create();

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