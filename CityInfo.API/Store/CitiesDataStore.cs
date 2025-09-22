using CityInfo.API.Data.Entities;

namespace CityInfo.API.Store;

public class CitiesDataStore
{
    public static CitiesDataStore Current { get; }  = new CitiesDataStore();
    public List<City> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities =
            [
            new City()
            {
                Id = 0,
                Name = "Vancouver",
                Description = "Rainy and cold, with mountains and beach.",
                PointsOfInterest = new List<PointOfInterest>()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Science World",
                        Description = "Geodesic dome with interactive exibits."
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Stanley Park",
                        Description = "Nice place to run with scenic seawall."
                    }
                }
            },
            new City()
            {
                Id = 10,
                Name = "Warsaw",
                Description = "Capital of Poland, a lot of sky scrapers.",
                PointsOfInterest =
				[
					new PointOfInterest()
                    {
                        Id = 11,
                        Name = "Copernicus Science Center",
                        Description = "Science Center with interactive exibits."
                    },
                    new PointOfInterest()
                    {
                        Id = 12,
                        Name = "Warsaw Uprising Museum",
                        Description = "Place devoted to uprising of 1944."
                    }
                ]
            },
            new City()
            {
                Id = 20,
                Name = "New York",
                Description = "The one with big park in the middle.",
                PointsOfInterest =
				[
					new PointOfInterest()
                    {
                        Id = 21,
                        Name = "Statue of Liberty",
                        Description = "French present, icon of New York harbor."
                    },
                    new PointOfInterest()
                    {
                        Id = 22,
                        Name = "Time Square",
                        Description = "Place where everybody wants to takes a photo."
                    }
                ]
            },
            new City()
            {
                Id = 30,
                Name = "Portland",
                Description = "Lets keep Portland weird sign.",
                PointsOfInterest =
				[
					new PointOfInterest()
                    {
                        Id = 31,
                        Name = "Hoyt Arboretum",
                        Description = "Tree garden with extensive hiking paths.",
                    },
                    new PointOfInterest()
                    {
                        Id = 32,
                        Name = "Pittock Mansion",
                        Description = "Museum shop with local flair.",
                    }
                ]
            },
            new City()
            {
                Id = 40,
                Name = "Poznan",
                Description = "Potato city, one of the largest in Poland.", 
                PointsOfInterest =
				[
					new PointOfInterest()
                    {
                        Id= 41,
                        Name = "Old Zoo",
                        Description = "Place with plenty of animals kept in captivity."
                    },
                    new PointOfInterest()
                    {
                        Id= 42,
                        Name = "Royal Castle",
                        Description = "Reconstructed, 13th century castle."
                    }
                ]
            },
            new City()
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
