using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Core.Contracts;

public interface IEmployeeService
{
    /// <summary>
    /// Gets all employees depending on currPage and perPage parameters.
    /// </summary>
    /// <param name="searchTerm">Searches in first name or last name of the employee.</param>
    /// <param name="sorting">EmployeeSorting enum value.</param>
    /// <param name="filter">EmployeeFilter enum value.</param>
    /// <param name="currPage">Used for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>Total employees and employees for this page.</returns>
    Task<AllResultModel<EmployeeViewModel>> All(string? searchTerm = null, EmployeeSorting sorting = EmployeeSorting.SalaryAsc, EmployeeFilter filter = EmployeeFilter.All, int currPage = 1, int perPage = 1);

    /// <summary>
    /// Gets all services for employee depending on currPage and perPage parameters.
    /// </summary>
    /// <param name="id">Id of the employee.</param>
    /// <param name="sorting">ServiceSorting enum value.</param>
    /// <param name="currPage">Used for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>Total services and services for this page.</returns>
    Task<AllResultModel<EmployeeServiceViewModel>> GetServices(int id, ServiceSorting sorting = ServiceSorting.All, int currPage = 1, int perPage = 1);
    
    /// <summary>
    /// Gets employee id.
    /// </summary>
    /// <param name="userId">Providing user id to find it's corresponding employee.</param>
    /// <returns>The id of the employee.</returns>
    Task<int> GetEmployeeId(string userId);
    
    /// <summary>
    /// Gets All employees first name and last name only.
    /// </summary>
    /// <returns>Collection of EmployeeForFormModel</returns>
    Task<IEnumerable<EmployeeForFormModel>> AllForForm();

    /// <summary>
    /// Adds employee to database.
    /// </summary>
    /// <param name="model">Input model from form.</param>
    /// <param name="user">Application user that corresponds to that employee.</param>
    /// <returns>Id of the employee, -1 if not succeeded.</returns>
    Task<int> Add(EmployeeInputModel model, ApplicationUser user);

    /// <summary>
    /// Check if employee exists.
    /// </summary>
    /// <param name="id">Providing employee's id that we want to check.</param>
    /// <returns>True if found, otherwise false.</returns>>
    Task<bool> Exists(int id);

    /// <summary>
    /// Get all roles.
    /// </summary>
    /// <returns>Collection of IdentityRole.</returns>
    Task<IEnumerable<IdentityRole>> GetAllRoles();

    /// <summary>
    /// Check if roles exists
    /// </summary>
    /// <param name="roles">Providing params of string to check in bulk.</param>
    /// <returns>True if all are found, otherwise false.</returns>>
    Task<bool> RolesExist(params string[] roles);

    /// <summary>
    /// Disables employee and deactivates it's account.
    /// </summary>
    /// <param name="id">Id of employee.</param>
    /// <returns>True if successful, otherwise false.</returns>>
    Task<bool> Fire(int id);

    /// <summary>
    /// Gets employee for edit.
    /// </summary>
    /// <param name="id">Id of the employee to edit.</param>
    /// <returns>EmployeeInputModel object.</returns>
    Task<EmployeeInputModel> GetEmployeeEdit(int id);

    /// <summary>
    /// Updates employee in the database.
    /// </summary>
    /// <param name="id">Id of the employee to update.</param>
    /// <param name="model">EmployeeInputModel with new parameters.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> Update(int id, EmployeeInputModel model);
}