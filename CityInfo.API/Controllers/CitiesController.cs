using CityInfo.API.Store;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("/api/cities")]
[EnableCors("AllowOrigin")]
public class CitiesController : Controller
{
    [HttpGet()]
    public IActionResult GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
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
