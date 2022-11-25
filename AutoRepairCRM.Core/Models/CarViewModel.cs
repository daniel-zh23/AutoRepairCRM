namespace AutoRepairCRM.Core.Models;

public class CarViewModel
{
    public int Id { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;
    
    public string Year { get; set; } = null!;
}