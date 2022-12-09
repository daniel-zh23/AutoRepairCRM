using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Database.Data.Models.Account;

public class ApplicationUser : IdentityUser
{
    [Required] 
    [MaxLength(DataConstants.Person.MaxCustomerFNameLength)]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [MaxLength(DataConstants.Person.MaxCustomerLNameLength)]
    public string LastName { get; set; } = null!;

    [Required]
    public bool IsFirstLogin { get; set; } = true;
    
    [Required]
    public bool IsActive { get; set; } = true;
}