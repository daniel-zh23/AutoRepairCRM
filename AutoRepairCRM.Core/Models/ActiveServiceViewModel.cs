namespace AutoRepairCRM.Core.Models;

public class ActiveServiceViewModel
{
    public int Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string CarLicense { get; set; } = null!;

    public string CustomerPhone { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string DateStarted { get; set; } = null!;
    
}