using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Core.Models.Services;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Core.Contracts;

public interface IEmployeeService
{
    Task<AllResultModel<EmployeeViewModel>> All(string? searchTerm = null, EmployeeSorting sorting = EmployeeSorting.SalaryAsc, int currPage = 1, int perPage = 1);

    Task<AllResultModel<EmployeeServiceViewModel>> GetServices(int id, ServiceSorting sorting = ServiceSorting.All, int currPage = 1, int perPage = 1);
    
    Task<int> GetEmployeeId(string userId);
    Task<IEnumerable<EmployeeForFormModel>> AllForForm();

    Task<string?> Add(EmployeeInputModel model);

    Task<bool> Exists(int id);

    Task<IEnumerable<IdentityRole>> GetAllRoles();

    Task<bool> RolesExist(params string[] roles);
}