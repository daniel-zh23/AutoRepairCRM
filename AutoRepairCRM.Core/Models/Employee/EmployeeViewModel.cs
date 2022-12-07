namespace AutoRepairCRM.Core.Models.Employee;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Salary { get; set; } = null!;

    public IEnumerable<string> Roles { get; set; } = new List<string>();
}