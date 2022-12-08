using System.ComponentModel.DataAnnotations;

namespace AutoRepairCRM.Core.Models.Services;

public class ServiceCompleteModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}