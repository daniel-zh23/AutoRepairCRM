using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository _repo;
    private readonly IAccountService _accountService;
    
    public EmployeeService(IRepository repo, IAccountService accountService)
    {
        _repo = repo;
        _accountService = accountService;
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

    public async Task<string?> Add(EmployeeInputModel model)
    {
        ApplicationUser user;
        try
        {
           user = await _accountService.CreateEmployee(model);
        }
        catch (Exception)
        {
            return null;
        }

        var employee = new Employee()
        {
            User = user,
            Salary = model.Salary
        };
        await _repo.AddAsync(employee);
        await _repo.SaveChangesAsync();

        return user.Id;
    }

    public async Task<bool> Exists(int id)
    {
        return await _repo.AllReadonly<Employee>()
            .AnyAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<IdentityRole>> GetRoles()
    {
        return await _repo.AllReadonly<IdentityRole>()
            .ToListAsync();
    }

    public async Task<bool> RolesExist(params string[] roles)
    {
        var rolesDb = await _repo.AllReadonly<IdentityRole>()
            .Select(r => r.Name)
            .ToListAsync();

        return roles.All(role => rolesDb.Contains(role));
    }
}