using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRepairCRM.Database.Data.Constants;
using AutoRepairCRM.Database.Data.Models.Account;

namespace AutoRepairCRM.Database.Data.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }

    [Required]
    [Column(TypeName = DataConstants.Employee.EmployeeSalaryDecimal)]
    public decimal Salary { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
    
    [Required]
    [Range(0, 50)]
    public int BonusPercent { get; set; }
    public ApplicationUser? User { get; set; }

    public IEnumerable<ServiceEmployee> ServicesEmployees { get; set; } = new List<ServiceEmployee>();
}