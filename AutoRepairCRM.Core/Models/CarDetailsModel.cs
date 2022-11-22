namespace AutoRepairCRM.Core.Models;

public class CarDetailsModel
{
    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public IEnumerable<ServiceViewModel> Services { get; set; } = new List<ServiceViewModel>();
}