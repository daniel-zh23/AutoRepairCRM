namespace AutoRepairCRM.Core.Models.Revenue;

public class RevenueModel
{
    public decimal? RevenueThisMonth { get; set; }

    public decimal? Profit { get; set; }

    public IEnumerable<EmployeeRevenueModel> Employees { get; set; } = new List<EmployeeRevenueModel>();
}