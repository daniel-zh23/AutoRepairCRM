using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Common;

namespace AutoRepairCRM.Database.Models;

public class ServiceType
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [MaxLength(DataConstants.ServiceType.MaxServiceTypeLength)]
    public string Name { get; set; } = null!;
}