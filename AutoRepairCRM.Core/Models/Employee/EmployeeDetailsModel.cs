using AutoRepairCRM.Core.Models.Services;

namespace AutoRepairCRM.Core.Models.Employee;

public class EmployeeDetailsModel
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public IEnumerable<CustomerServiceViewModel> Services { get; set; } = new List<CustomerServiceViewModel>();
}