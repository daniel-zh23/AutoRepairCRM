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
        EmployeeSorting sorting = EmployeeSorting.Newest, EmployeeFilter filter = EmployeeFilter.All, int currPage = 1,
        int perPage = 1)
    {
        var result = new AllResultModel<EmployeeViewModel>();
        var employees = _repo.AllReadonly<Employee>()
            .Where(e => e.IsActive);

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

        var materializedEmployees = await employees
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.User!.FirstName,
                LastName = e.User!.LastName,
                Salary = e.Salary.ToString("f2"),
                User = e.User
            })
            .ToListAsync();
        
        foreach (var employeeViewModel in materializedEmployees)
        {
            employeeViewModel.Roles = await _accountService.GetRolesForUser(employeeViewModel.User);
        }

        materializedEmployees = filter switch
        {
            EmployeeFilter.Office => materializedEmployees.Where(e => e.Roles.Contains("OfficeEmployee")).ToList(),
            EmployeeFilter.Worker => materializedEmployees.Where(e => e.Roles.Contains("Worker")).ToList(),
            _ => materializedEmployees
        };
        
        result.People = materializedEmployees;
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
            .Select(s => new EmployeeServiceViewModel
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
            .Where(e => e.IsActive)
            .Select(e => new EmployeeForFormModel
            {
                Id = e.Id,
                FirstName = e.User == null ? "" : e.User.FirstName,
                LastName = e.User == null ? "" : e.User.LastName
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

        var employee = new Employee
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

    public async Task<bool> Fire(int id)
    {
        var employee = await _repo.GetByIdAsync<Employee>(id);
        var user = await _repo.GetByIdAsync<ApplicationUser>(employee.UserId!);

        employee.IsActive = false;
        await _accountService.Deactivate(user.Id);
        
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<EmployeeInputModel> GetEmployeeEdit(int id)
    {
        var employee = await _repo.GetByIdAsync<Employee>(id);
        var user = await _repo.GetByIdAsync<ApplicationUser>(employee.UserId!);

        return new EmployeeInputModel
        {
            Id = employee.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Salary = employee.Salary,
            Roles = _accountService.GetRolesForUser(user).Result.First()
        };
    }

    public async Task<bool> Update(int id, EmployeeInputModel model)
    {
        var employee = await _repo.AllReadonly<Employee>()
            .Where(c => c.Id == id)
            .FirstAsync();
        var user = await _repo.GetByIdAsync<ApplicationUser>(employee.UserId!);
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.Phone;
        user.Email = model.Email;
        employee.Salary = model.Salary;

        _repo.Update(employee);
        await _repo.SaveChangesAsync();
        return true;
    }
}