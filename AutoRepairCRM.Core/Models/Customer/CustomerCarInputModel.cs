using System.ComponentModel.DataAnnotations;

namespace AutoRepairCRM.Core.Models.Customer;

public class CustomerCarInputModel
{
    [Required]
    public int CarId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public string LicensePlate { get; set; } = null!;

    [Required]
    public string Litre { get; set; } = null!;

    [Required]
    public int FuelTypeId { get; set; }
}