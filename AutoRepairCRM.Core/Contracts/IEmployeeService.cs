using AutoRepairCRM.Core.Models.Employee;

namespace AutoRepairCRM.Core.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeForFormModel>> GetEmployees();

    Task<bool> Exists(int id);
}