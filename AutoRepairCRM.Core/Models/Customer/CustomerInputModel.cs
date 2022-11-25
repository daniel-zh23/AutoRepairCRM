using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Areas.Admin.Constants;

namespace AutoRepairCRM.Core.Models.Customer;

public class CustomerInputModel
{
    [Required]
    [StringLength(DataConstants.Person.MaxCustomerFNameLength,
        MinimumLength = DataConstants.Person.MinCustomerFNameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(DataConstants.Person.MaxCustomerLNameLength, MinimumLength = DataConstants.Person.MinCustomerLNameLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(DataConstants.Person.MaxCustomerPhoneLength)]
    public string Phone { get; set; } = null!;
    
    [Required]
    public int CarId { get; set; }
}