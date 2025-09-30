using CityInfo.API.Application;
using CityInfo.API.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("/api/cities")]
[ApiController]
[EnableCors("AllowOrigin")]
public class PointsOfInterestController(ICityInfoService cityInfoService) : Controller
{
	private readonly ICityInfoService _cityInfoService =
		cityInfoService ?? throw new ArgumentNullException(nameof(cityInfoService));

	[HttpGet("{cityId}/pointsofinterest")]
    public async Task<IActionResult> GetPointsOfInterest(int cityId)
    {
        var foundCity = await _cityInfoService.GetCityAsync(cityId, true, CancellationToken.None);
		if (foundCity == null)
            return NotFound();

        return Ok(foundCity.PointsOfInterest);
    }
    
    [HttpGet("{cityId}/pointofinterest/{id}", Name = "GetPointOfInterest")]
    public async Task<IActionResult> GetPointOfInterest(int cityId, int id)
    {
        var foundCity = await _cityInfoService.GetCityAsync(cityId, true, CancellationToken.None);
		if (foundCity == null)
            return NotFound();

        var foundPointOfInterest = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == id);
        if (foundPointOfInterest == null)
            return NotFound();
        return Ok(foundPointOfInterest);
    }

	[HttpPost("{cityId}/pointsofinterest")]
	public async Task<IActionResult> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreation pointOfInterest)
	{
		if (pointOfInterest is null)
			return BadRequest();

		if (pointOfInterest.Description != null && pointOfInterest.Name != null &&
			pointOfInterest.Description.Equals(pointOfInterest.Name))
		{
			ModelState.AddModelError("Description", "Description should be different from the name.");
		}

		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var foundCity = await _cityInfoService.GetCityAsync(cityId, false, CancellationToken.None);
		if (foundCity == null)
		{
			return NotFound();
		}

        // Create the DTO for the service
        var createPointOfInterestDto = new CreatePointOfInterestDto(
            pointOfInterest.Name ?? string.Empty, 
            pointOfInterest.Description ?? string.Empty);

		// Use the service to add the point of interest
		var createdPointOfInterest = await _cityInfoService.AddPointOfInterestAsync(
            cityId, 
            createPointOfInterestDto, 
            CancellationToken.None);

		if (createdPointOfInterest == null)
		{
			return BadRequest("Failed to create point of interest.");
		}

		return CreatedAtRoute("GetPointOfInterest", new { cityId, id = createdPointOfInterest.Id }, createdPointOfInterest);
	}

	[HttpPut("{cityId}/pointsofinterest/{poiId}")]
    public async Task<IActionResult> UpdatePointOfInterest(int cityId, int poiId, [FromBody] PointOfInterestForUpdate pointOfInterest)
    {
        if (pointOfInterest == null)
            return BadRequest();

        if (pointOfInterest.Description.Equals(pointOfInterest.Name))
            ModelState.AddModelError("Description", "Description should be different from the name.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        var foundCity = await _cityInfoService.GetCityAsync(cityId, true, CancellationToken.None);

		if (foundCity == null)
            return NotFound();

        var poi = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
        if (poi == null)
            return NotFound();

        //poi.Name = pointOfInterest.Name;
        //poi.Description = pointOfInterest.Description;
        var updatePointOfInterestDto = new UpdatePointOfInterestDto(
            pointOfInterest.Name ?? string.Empty, 
            pointOfInterest.Description ?? string.Empty);

        await _cityInfoService.UpdatePointOfInterestAsync(cityId, poiId, updatePointOfInterestDto, CancellationToken.None);

		return NoContent(); //204 => can also return 200OK and pass updated resource;
    }

    [HttpDelete("{cityId}/pointsofinterest/{poiId}")]
    public async Task<IActionResult> DeletePointOfInterest(int cityId, int poiId)
    {
        //var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        var foundCity = await _cityInfoService.GetCityAsync(cityId, true, CancellationToken.None);

        
        if (foundCity == null)
            return NotFound();

        var poi = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
        if (poi == null)
            return NotFound();

        foundCity.PointsOfInterest.Remove(poi);

        return NoContent();

    }
}
