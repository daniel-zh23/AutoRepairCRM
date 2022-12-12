using AutoRepairCRM.Core.Models.Revenue;

namespace AutoRepairCRM.Core.Contracts;

public interface IRevenueService
{
    /// <summary>
    /// Gets revenue information and all employees with salaries
    /// </summary>
    /// <param name="date">DateTime to get desired revenue search for the provided month and year.</param>
    /// <returns>Returns revenue for the month, profit and collection of EmployeeRevenueModel</returns>
    Task<RevenueModel> GetRevenue(DateTime date);
}