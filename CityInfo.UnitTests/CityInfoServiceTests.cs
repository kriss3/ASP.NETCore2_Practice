using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using CityInfo.API.Application;
using CityInfo.API.Entities;
using FakeItEasy;

namespace CityInfo.UnitTests;

/* Notes: In AutoFixture using interface vs concrete class:
 * When you use the interface (ICityInfoService), AutoFixture with AutoFakeItEasyCustomization
 * creates a fake implementation that doesn't use your real service logic. 
 * When you use the concrete class (CityInfoService), AutoFixture creates a real instance 
 * that depends on the frozen fake repository, allowing your repository setup to work correctly.
 */

public class CityInfoServiceTests
{
	private readonly IFixture _fixture;

	public CityInfoServiceTests()
	{
		_fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
	}

	[Fact]
	public async Task GetCity_Return_Null_When_City_Not_Found()
	{


		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();
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

	[Fact]
	public async Task GetCities_Returns_Empty_List_When_No_Cities_Found()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();

		A.CallTo(() => repo.GetCitiesAsync(A<CancellationToken>._))
			.Returns([]);

		// Act
		var result = await service.GetCitiesAsync(CancellationToken.None);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public async Task GetCities_Returns_List_Of_CitySummaryDto_When_Cities_Found()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();

		var cities = _fixture.Build<City>()
			.With(c => c.PointsOfInterest, [])
			.CreateMany(3)
			.ToList();

		A.CallTo(() => repo.GetCitiesAsync(A<CancellationToken>._))
			.Returns(cities);

		// Act
		var result = await service.GetCitiesAsync(CancellationToken.None);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(3, result.Count);
		Assert.All(result, dto =>
		{
			Assert.NotEqual(0, dto.Id);
			Assert.NotEmpty(dto.Name);
		});

		A.CallTo(() => repo.GetCitiesAsync(A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task AddCity_Returns_CityDto_When_City_Added_Successfully()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();

		var createCityDto = _fixture.Build<CreateCityDto>()
			.With(c => c.Name, "Tokyo")
			.With(c => c.Description, "Capital of Japan")
			.Create();

		var addedCity = _fixture.Build<City>()
			.With(c => c.Id, 5)
			.With(c => c.Name, "Tokyo")
			.With(c => c.Description, "Capital of Japan")
			.With(c => c.PointsOfInterest, [])
			.Create();

		A.CallTo(() => repo.AddCityAsync(A<City>._, A<CancellationToken>._))
			.Returns(addedCity);

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.Returns(1);

		// Act
		var result = await service.AddCityAsync(createCityDto, CancellationToken.None);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(5, result.Id);
		Assert.Equal("Tokyo", result.Name);
		Assert.Equal("Capital of Japan", result.Description);

		A.CallTo(() => repo.AddCityAsync(A<City>.That.Matches(c =>
			c.Name == "Tokyo" && c.Description == "Capital of Japan"), A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task AddPointOfInterest_Returns_Null_When_City_Not_Found()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();
		var cityId = _fixture.Create<int>();

		var createPointDto = _fixture.Build<CreatePointOfInterestDto>()
			.With(p => p.Name, "Eiffel Tower")
			.With(p => p.Description, "Iconic landmark")
			.Create();

		A.CallTo(() => repo.GetCityAsync(cityId, true, A<CancellationToken>._))
			.Returns((City?)null);

		// Act
		var result = await service.AddPointOfInterestAsync(cityId, createPointDto, CancellationToken.None);

		// Assert
		Assert.Null(result);
		A.CallTo(() => repo.AddPointAsync(A<City>._, A<PointOfInterest>._, A<CancellationToken>._))
			.MustNotHaveHappened();
	}

	[Fact]
	public async Task AddPointOfInterest_Returns_PointOfInterestDto_When_Successfully_Added()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();
		var cityId = 1;

		var createPointDto = _fixture.Build<CreatePointOfInterestDto>()
			.With(p => p.Name, "Eiffel Tower")
			.With(p => p.Description, "Iconic landmark")
			.Create();

		var city = _fixture.Build<City>()
			.With(c => c.Id, cityId)
			.With(c => c.PointsOfInterest, [])
			.Create();

		A.CallTo(() => repo.GetCityAsync(cityId, true, A<CancellationToken>._))
			.Returns(city);

		A.CallTo(() => repo.AddPointAsync(city, A<PointOfInterest>._, A<CancellationToken>._))
			.Returns(Task.CompletedTask);

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.Returns(1);

		// Act
		var result = await service.AddPointOfInterestAsync(cityId, createPointDto, CancellationToken.None);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Eiffel Tower", result.Name);
		Assert.Equal("Iconic landmark", result.Description);

		A.CallTo(() => repo.AddPointAsync(city, A<PointOfInterest>.That.Matches(p =>
			p.Name == "Eiffel Tower" && p.Description == "Iconic landmark"), A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task UpdatePointOfInterest_Returns_False_When_Point_Not_Found()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();
		var cityId = _fixture.Create<int>();
		var pointId = _fixture.Create<int>();

		var updatePointDto = _fixture.Build<UpdatePointOfInterestDto>()
			.With(p => p.Name, "Updated Name")
			.With(p => p.Description, "Updated Description")
			.Create();

		A.CallTo(() => repo.GetPointAsync(cityId, pointId, A<CancellationToken>._))
			.Returns((PointOfInterest?)null);

		// Act
		var result = await service.UpdatePointOfInterestAsync(cityId, pointId, updatePointDto, CancellationToken.None);

		// Assert
		Assert.False(result);
		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.MustNotHaveHappened();
	}

	[Fact]
	public async Task UpdatePointOfInterest_Returns_True_When_Successfully_Updated()
	{
		// Arrange
		var repo = _fixture.Freeze<ICityInfoRepository>();
		var service = _fixture.Create<CityInfoService>();
		var cityId = 1;
		var pointId = 10;

		var updatePointDto = _fixture.Build<UpdatePointOfInterestDto>()
			.With(p => p.Name, "Updated Tower")
			.With(p => p.Description, "Updated landmark")
			.Create();

		var existingPoint = _fixture.Build<PointOfInterest>()
			.With(p => p.Id, pointId)
			.With(p => p.Name, "Original Tower")
			.With(p => p.Description, "Original landmark")
			.Create();

		A.CallTo(() => repo.GetPointAsync(cityId, pointId, A<CancellationToken>._))
			.Returns(existingPoint);

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.Returns(1);

		// Act
		var result = await service.UpdatePointOfInterestAsync(cityId, pointId, updatePointDto, CancellationToken.None);

		// Assert
		Assert.True(result);
		Assert.Equal("Updated Tower", existingPoint.Name);
		Assert.Equal("Updated landmark", existingPoint.Description);

		A.CallTo(() => repo.SaveChangesAsync(A<CancellationToken>._))
			.MustHaveHappenedOnceExactly();
	}

	[Fact]
	public async Task DeletePointOfInterest_Returns_False_When_Point_Not_Found()
	{
	}

	[Fact]
	public async Task DeletePointOfInterest_Returns_True_When_Successfully_Deleted()
	{
	}

}
