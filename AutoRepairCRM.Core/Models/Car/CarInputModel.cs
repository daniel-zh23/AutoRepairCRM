using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Core.Models.Car;

public class CarInputModel
{
    [Required]
    [MaxLength(DataConstants.Car.MaxCarMakeLength)]
    public string Make { get; set; } = null!;

    [MaxLength(DataConstants.Car.MaxCarModelLength)]
    [Required]
    public string Model { get; set; } = null!;
    
    [Required]
    [MaxLength(DataConstants.Car.MaxCarYearLength)]
    public string Year { get; set; } = null!;
}