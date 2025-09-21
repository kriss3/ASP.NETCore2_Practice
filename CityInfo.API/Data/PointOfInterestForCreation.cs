using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Data;

public class PointOfInterestForCreation
{
    [Required(ErrorMessage ="You should provide a name value.")]
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [MaxLength(200)]
    public required string Description { get; set; }
}
