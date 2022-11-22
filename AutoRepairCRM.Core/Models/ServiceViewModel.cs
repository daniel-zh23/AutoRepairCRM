namespace AutoRepairCRM.Core.Models;

public class ServiceViewModel
{
    public bool ServiceState { get; set; }

    public string ServiceType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Price { get; set; }
}