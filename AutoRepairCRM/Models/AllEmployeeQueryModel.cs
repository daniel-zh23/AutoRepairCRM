using AutoRepairCRM.Core.Models.Employee;

namespace AutoRepairCRM.Models;

public class AllEmployeeQueryModel
{
    public const int PeoplePerPage = 5;

    public string? SearchTerm { get; set; }

    public EmployeeSorting Sorting { get; set; }
    
    public EmployeeFilter Filter { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int Total { get; set; }

    public IEnumerable<EmployeeViewModel> People { get; set; } = Enumerable.Empty<EmployeeViewModel>();
}