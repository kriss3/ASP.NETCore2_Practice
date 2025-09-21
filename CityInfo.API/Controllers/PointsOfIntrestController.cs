using CityInfo.API.Data;
using CityInfo.API.Models;
using CityInfo.API.Store;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("/api/cities")]
[ApiController]
[EnableCors("AllowOrigin")]
public class PointsOfInterestController : Controller
{
    [HttpGet("{cityId}/pointsofinterest")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (foundCity == null)
            return NotFound();

        return Ok(foundCity.PointsOfInterest);
    }
    
    [HttpGet("{cityId}/pointofinterest/{id}", Name = "GetPointOfInterest")]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (foundCity == null)
            return NotFound();

        var foundPointOfInterest = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == id);
        if (foundPointOfInterest == null)
            return NotFound();
        return Ok(foundPointOfInterest);
    }
    
    [HttpPost("{cityId}/pointsofinterest")]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreation pointOfInterest)
    {
        if (pointOfInterest == null)
        {
            BadRequest();
        }

        if (pointOfInterest.Description.Equals(pointOfInterest.Name))
            ModelState.AddModelError("Description", "Description should be different from the name.");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (foundCity == null)
        {
            NotFound();
        }

        var maxIdOfPointOfInterest = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(m => m.Id);

        var finalPointOInterest = new PointOfInterest()
        {
            Id = ++maxIdOfPointOfInterest,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };

        foundCity.PointsOfInterest.Add(finalPointOInterest);
        return CreatedAtRoute("GetPointOfInterest", new { cityId, id = finalPointOInterest.Id }, finalPointOInterest);
    }

    [HttpPut("{cityId}/pointsofinterest/{poiId}")]
    public IActionResult UpdatePointOfInterest(int cityId, int poiId, [FromBody] PointOfInterestForUpdate pointOfInterest)
    {
        if (pointOfInterest == null)
            return BadRequest();

        if (pointOfInterest.Description.Equals(pointOfInterest.Name))
            ModelState.AddModelError("Description", "Description should be different from the name.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (foundCity == null)
            NotFound();
        var poi = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
        if (poi == null)
            NotFound();

        poi.Name = pointOfInterest.Name;
        poi.Description = pointOfInterest.Description;

        return NoContent(); //204 => can also return 200OK and pass updated resource;
    }

    [HttpDelete("{cityId}/pointsofinterest/{poiId}")]
    public IActionResult DeletePointOfInterest(int cityId, int poiId)
    {
        var foundCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (foundCity == null)
            return NotFound();

        var poi = foundCity.PointsOfInterest.FirstOrDefault(p => p.Id == poiId);
        if (poi == null)
            return NotFound();

        foundCity.PointsOfInterest.Remove(poi);

        return NoContent();

    }
}
