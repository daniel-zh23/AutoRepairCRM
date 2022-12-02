using AutoRepairCRM.Core.Models.Employee;

namespace AutoRepairCRM.Core.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeForFormModel>> GetEmployees();
}