﻿using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class Car
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(DataConstants.Car.MaxCarMakeLength)]
    public string Make { get; set; } = null!;

    [MaxLength(DataConstants.Car.MaxCarModelLength)]
    [Required] public string Model { get; set; } = null!;

    [MaxLength(DataConstants.Car.MaxCarEngineNumberLength)]
    [Required] public string EngineNumber { get; set; } = null!;
    
}