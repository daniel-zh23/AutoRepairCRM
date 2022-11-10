using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairCRM.Database.Models;

public class ServiceEmployee
{
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }

    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }

    public Service Service { get; set; } = null!;

    public Employee Employee { get; set; } = null!;
}