using CityInfo.API.Application;
//using CityInfo.API.Store;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("/api/cities")]
[ApiController]
[EnableCors("AllowOrigin")]
public class CitiesController(ICityInfoService cityInfoService) : Controller
{
    private readonly ICityInfoService _cityInfoService = 
        cityInfoService ?? throw new ArgumentNullException(nameof(cityInfoService));

	[HttpGet()]
    public async Task<IActionResult> GetCities()
    {
		//return Ok(CitiesDataStore.Current.Cities);
		var cities = await _cityInfoService.GetCitiesAsync(CancellationToken.None);
		return Ok(cities);
	}

    [HttpGet("{id}")]
    public IActionResult GetCity(int id)
    {
        //find city;
        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c=>c.Id == id);
        if (foundCity == null)
            return NotFound();
        return Ok(foundCity);
    }
}
