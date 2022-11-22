using System.ComponentModel.DataAnnotations;

namespace AutoRepairCRM.Database.Data.Models;

public class FuelType
{
    [Key]
    public int Id { get; set; }

    [Required] 
    public string Name { get; set; } = null!;
}