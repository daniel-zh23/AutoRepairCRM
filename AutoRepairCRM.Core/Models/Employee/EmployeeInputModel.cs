using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Areas.Admin.Constants;

namespace AutoRepairCRM.Core.Models.Employee;

public class EmployeeInputModel
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(DataConstants.Person.MaxCustomerFNameLength,
        MinimumLength = DataConstants.Person.MinCustomerFNameLength)]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(DataConstants.Person.MaxCustomerLNameLength, MinimumLength = DataConstants.Person.MinCustomerLNameLength)]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(DataConstants.Person.MaxCustomerPhoneLength)]
    [Display(Name = "Phone number")]
    public string Phone { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [Range(0, 50, ErrorMessage = "Bonus can be between 0% and 50% (real number)!")]
    public int Bonus { get; set; }
    
    [Required]
    [Display(Name = "Base salary")]
    public decimal Salary { get; set; }

    public string Roles { get; set; } = null!;
}