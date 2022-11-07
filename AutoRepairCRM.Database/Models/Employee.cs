using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRepairCRM.Database.Common;

namespace AutoRepairCRM.Database.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(DataConstants.Person.MaxCustomerFNameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(DataConstants.Person.MaxCustomerLNameLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = DataConstants.Employee.EmployeeSalaryDecimal)]
    public decimal Salary { get; set; }

    public IEnumerable<ServiceEmployee> ServicesEmployees { get; set; } = new List<ServiceEmployee>();
}