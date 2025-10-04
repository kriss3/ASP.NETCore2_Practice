using AutoFixture.Xunit3;
using CityInfo.API.Application;
using CityInfo.API.Entities;

using Xunit;
using FakeItEasy;


namespace CityInfo.UnitTests;
public class CityInfoServiceTests
{
	[Theory, AutoFakeData]
	public async Task GetCity_returns_dto_when_found(
		[Frozen] ICityInfoRepository repo,
		CityInfoService sut,
		City city)
	{
		city.PointsOfInterest ??= new List<PointOfInterest>();
		A.CallTo(() => repo.GetCityAsync(42, true, A<CancellationToken>._)).Returns(city);

		var dto = await sut.GetCityAsync(42, includePointsOfInterest: true, default);

		Assert.NotNull(dto);
		Assert.Equal(city.Id, dto!.Id);
		Assert.Equal(city.Name, dto.Name);
		Assert.Equal(city.Description, dto.Description);
	}
}
