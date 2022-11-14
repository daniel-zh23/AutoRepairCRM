using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class ServiceType
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [MaxLength(DataConstants.ServiceType.MaxServiceTypeLength)]
    public string Name { get; set; } = null!;
}