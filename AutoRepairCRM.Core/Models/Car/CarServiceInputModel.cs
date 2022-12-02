using System.ComponentModel.DataAnnotations;

namespace AutoRepairCRM.Core.Models.Car;

public class CarServiceInputModel
{
    [Required]
    public int CarId { get; set; }
    
    [Required]
    public int CustomerId { get; set; }
    
    [Required]
    public int ServiceTypeId { get; set; }
    
    public DateTime DateStarted { get; set; }

    public IEnumerable<int> Employees { get; set; } = new List<int>();
}