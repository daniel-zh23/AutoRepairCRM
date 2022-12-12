using AutoRepairCRM.Core.Models.Revenue;

namespace AutoRepairCRM.Models;

public class AllRevenueQueryModel
{
    public int Month { get; set; } = DateTime.UtcNow.Month;

    public int Year { get; set; } = DateTime.UtcNow.Year;

    public decimal? Revenue { get; set; }

    public decimal? Profit { get; set; }

    public IEnumerable<EmployeeRevenueModel> Employees { get; set; } = Enumerable.Empty<EmployeeRevenueModel>();
}