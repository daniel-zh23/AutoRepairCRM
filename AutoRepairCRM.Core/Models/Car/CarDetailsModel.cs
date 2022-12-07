using AutoRepairCRM.Core.Models.Services;

namespace AutoRepairCRM.Core.Models.Car;

public class CarDetailsModel
{
    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public IEnumerable<CustomerServiceViewModel> Services { get; set; } = new List<CustomerServiceViewModel>();
}