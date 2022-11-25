namespace AutoRepairCRM.Core.Models;

public class CustomerCarViewModel
{
    public int CustomerId { get; set; }

    public int CarId { get; set; }
    public string Make { get; set; } = null!;
    
    public string Model { get; set; } = null!;

    public string Year { get; set; } = null!;

    public string Litre { get; set; } = null!;
    
    public string LicensePlate { get; set; } = null!;

    public string FuelType { get; set; } = null!;
}