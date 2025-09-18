using CityInfo.API.Models;
using System.Collections.Generic;

namespace CityInfo.API.Store;

public class CitiesDataStore
{
    public static CitiesDataStore Current { get; }  = new CitiesDataStore();
    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities =
			[
				new CityDto()
            {
                Id = 0,
                Name = "Vancouver",
                Description = "Rainy and cold, with mountains and beach.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Science World",
                        Description = "Geodesic dome with interactive exibits."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Stanley Park",
                        Description = "Nice place to run with scenic seawall."
                    }
                }
            },
            new CityDto()
            {
                Id = 10,
                Name = "Warsaw",
                Description = "Capital of Poland, a lot of sky scrapers.",
                PointsOfInterest =
				[
					new PointOfInterestDto()
                    {
                        Id = 11,
                        Name = "Copernicus Science Center",
                        Description = "Science Center with interactive exibits."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 12,
                        Name = "Warsaw Uprising Museum",
                        Description = "Place devoted to uprising of 1944."
                    }
                ]
            },
            new CityDto()
            {
                Id = 20,
                Name = "New York",
                Description = "The one with big park in the middle.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 21,
                        Name = "Statue of Liberty",
                        Description = "French present, icon of New York harbor."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 22,
                        Name = "Time Square",
                        Description = "Place where everybody wants to takes a photo."
                    }
                }
            },
            new CityDto()
            {
                Id = 30,
                Name = "Portland",
                Description = "Lets keep Portland weird sign.",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 31,
                        Name = "Hoyt Arboretum",
                        Description = "Tree garden with extensive hiking paths.",
                    },
                    new PointOfInterestDto()
                    {
                        Id = 32,
                        Name = "Pittock Mansion",
                        Description = "Museum shop with local flair.",
                    }
                }
            },
            new CityDto()
            {
                Id = 40,
                Name = "Poznan",
                Description = "Potato city, one of the largest in Poland.", 
                PointsOfInterest =
				[
					new PointOfInterestDto()
                    {
                        Id= 41,
                        Name = "Old Zoo",
                        Description = "Place with plenty of animals kept in captivity."
                    },
                    new PointOfInterestDto()
                    {
                        Id= 42,
                        Name = "Royal Castle",
                        Description = "Reconstructed, 13th century castle."
                    }
                ]
            },
            new CityDto()
            {
                Id = 50,
                Name = "Los Angeles",
                Description = "The city of gangs, violence and Hollywood.", 
                PointsOfInterest =
				[
					new()
                    {
                        Id = 51,
                        Name = "Hollywood Sign",
                        Description  = "The big sign on the side of the hill."
                    },
                    new()
                    {
                        Id = 52,
                        Name = "Observation tower",
                        Description  = "The famous one from GTA V game."
                    }
                ]
            }
        ];
    }
}
