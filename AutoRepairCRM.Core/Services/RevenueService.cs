using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Revenue;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class RevenueService : IRevenueService
{
    private readonly IRepository _repo;

    public RevenueService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<RevenueModel> GetRevenue(DateTime date)
    {
        var result = new RevenueModel();
        result.RevenueThisMonth =  await _repo.AllReadonly<Service>()
            .Where(s => s.DateEnded != null && s.DateEnded.Value.Month == date.Month && s.DateEnded.Value.Year == date.Year)
            .Select(s => s.Price).SumAsync();

        var employees = await _repo.AllReadonly<Employee>()
            .Include(e => e.User)
            .ToListAsync();

        var employeeBonus = await _repo.AllReadonly<ServiceEmployee>()
            .Include(se => se.Service)
            .Where(se => se.Service.DateEnded.Value.Month == date.Month && se.Service.DateEnded.Value.Year == date.Year)
            .Select(se => new { se.EmployeeId, se.Service.Price })
            .ToListAsync();

        result.Employees = employees.Select(e => new EmployeeRevenueModel
        {
            FirstName = e.User!.FirstName,
            LastName = e.User!.LastName,
            Salary = e.Salary,
            Total = e.Salary + (e.BonusPercent != 0 ?
                (employeeBonus.Where(emp => emp.EmployeeId == e.Id).Select(s => (e.BonusPercent / 100.0m) * s.Price).Sum()) ?? 0m 
                : 0),
            BonusPercentage = e.BonusPercent
        });

        result.Profit = result.RevenueThisMonth - result.Employees.Sum(e => e.Total);
        
        return result;
    }
}