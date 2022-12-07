using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Core.Models.Services;
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

    public async Task<AllResultModel<EmployeeViewModel>> All(string? searchTerm = null,
        EmployeeSorting sorting = EmployeeSorting.Newest, int currPage = 1,
        int perPage = 1)
    {
        var result = new AllResultModel<EmployeeViewModel>();
        var employees = _repo.AllReadonly<Employee>();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = $"%{searchTerm.ToLower()}%";

            employees = employees
                .Where(e => EF.Functions.Like(e.User!.FirstName, searchTerm) ||
                            EF.Functions.Like(e.User!.LastName, searchTerm));
        }

        employees = sorting switch
        {
            EmployeeSorting.SalaryAsc => employees.OrderBy(e => e.Salary),
            EmployeeSorting.SalaryDesc => employees.OrderByDescending(e => e.Salary),
            _ => employees.OrderByDescending(e => e.Id)
        };

        result.People = await employees
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(e => new EmployeeViewModel()
            {
                Id = e.Id,
                FirstName = e.User!.FirstName,
                LastName = e.User!.LastName,
                Salary = e.Salary.ToString("f2")
            })
            .ToListAsync();

        result.Total = await employees.CountAsync();
        return result;
    }

    public async Task<AllResultModel<EmployeeServiceViewModel>> GetServices(int id, ServiceSorting sorting = ServiceSorting.All, int currPage = 1, int perPage = 1)
    {
        var result = new AllResultModel<EmployeeServiceViewModel>();
        var services = _repo.AllReadonly<ServiceEmployee>()
            .Where(s => s.EmployeeId == id);

        services = sorting switch
        {
            ServiceSorting.Finished => services.Where(s => s.Service.IsFinished == true),
            ServiceSorting.NotFinished => services.Where(s => s.Service.IsFinished == false),
            _ => services
        };

        result.Total = await services.CountAsync();
        result.People = await services
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(s => new EmployeeServiceViewModel()
            {
                LicensePlate = s.Service.CustomerCar.LicensePlate,
                ServiceType = s.Service.ServiceType.Name,
                ServiceState = s.Service.IsFinished,
                StartDate = s.Service.DateStarted.ToString("dd-MM-yyyy"),
                EndDate = s.Service.DateEnded == null ? "" : s.Service.DateEnded.Value.ToString("dd-MM-yyyy")
            })
            .ToListAsync();

        return result;
    }

    public async Task<int> GetEmployeeId(string userId)
    {
        return await _repo.AllReadonly<Employee>(c => c.UserId == userId)
            .Select(c => c.Id)
            .FirstAsync();
    }

    public async Task<IEnumerable<EmployeeForFormModel>> AllForForm()
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

    public async Task<IEnumerable<IdentityRole>> GetAllRoles()
    {
        return await _repo.AllReadonly<IdentityRole>()
            .Where(r => r.Name != "Owner" && r.Name != "Customer")
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