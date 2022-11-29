using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Areas.Admin.Constants;

namespace AutoRepairCRM.Core.Models.Customer;

public class CustomerInputModel
{
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
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(DataConstants.Person.MaxCustomerPhoneLength)]
    [Display(Name = "Phone number")]
    public string Phone { get; set; } = null!;
}