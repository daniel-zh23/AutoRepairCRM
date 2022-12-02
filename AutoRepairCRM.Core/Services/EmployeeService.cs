using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository _repo;
    
    public EmployeeService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<EmployeeForFormModel>> GetEmployees()
    {
        return await _repo.AllReadonly<Employee>()
            .Select(e => new EmployeeForFormModel()
            {
                Id = e.Id,
                FirstName = e.User == null ? "" : e.User.FirstName,
                LastName = e.User == null ? "" : e.User.LastName,
            })
            .ToListAsync();
    }
}