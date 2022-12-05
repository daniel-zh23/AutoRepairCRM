using AutoRepairCRM.Core.Models.Employee;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Core.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeForFormModel>> GetEmployees();

    Task<string?> Add(EmployeeInputModel model);

    Task<bool> Exists(int id);

    Task<IEnumerable<IdentityRole>> GetRoles();

    Task<bool> RolesExist(params string[] roles);
}