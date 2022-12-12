namespace AutoRepairCRM.Core.Models.Revenue;

public class EmployeeRevenueModel
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal Total { get; set; }

    public int BonusPercentage { get; set; }
}