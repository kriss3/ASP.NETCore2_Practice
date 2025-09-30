using CityInfo.API.Application;
using CityInfo.API.Entities;
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
		var cities = await _cityInfoService.GetCitiesAsync(CancellationToken.None);
		return Ok(cities);
	}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
        var foundCity = await _cityInfoService.GetCityAsync(id, false, CancellationToken.None);
		if (foundCity == null)
            return NotFound();
        return Ok(foundCity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CityForCreation cityForCreation)
    {
		if (cityForCreation is null)
			return BadRequest();

		// Validate model
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		// Create the DTO for the service
		var createCityDto = new CreateCityDto(
			cityForCreation.Name ?? string.Empty,
			cityForCreation.Description ?? string.Empty);

		// Use the service to add the city
		var createdCity = await _cityInfoService.AddCityAsync(createCityDto, CancellationToken.None);


	}
