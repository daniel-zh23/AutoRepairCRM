using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Services;

namespace AutoRepairCRM.Areas.Employees.Models;

public class AllForEmployeeModel
{
    public int TotalServices { get; set; }

    public const int ServicesPerPage = 5;

    public ServiceSorting Sorting { get; set; }
    public int CurrentPage { get; set; } = 1;

    public IEnumerable<EmployeeServiceViewModel> Services { get; set; } = Enumerable.Empty<EmployeeServiceViewModel>();
}