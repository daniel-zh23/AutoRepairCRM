using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class ServiceState
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(DataConstants.ServiceState.MaxServiceStateLength)]
    public string Name { get; set; } = null!;
}