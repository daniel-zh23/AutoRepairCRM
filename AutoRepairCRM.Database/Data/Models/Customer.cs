using System.ComponentModel.DataAnnotations;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(DataConstants.Person.MaxCustomerFNameLength)]
    public string FirstName { get; set; } = null!;

    [Required] 
    [MaxLength(DataConstants.Person.MaxCustomerLNameLength)]

    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(DataConstants.Person.MaxCustomerPhoneLength)]
    public string PhoneNumber { get; set; } = null!;

    [EmailAddress]
    public string? Email { get; set; }

    public IEnumerable<CustomerCar> CustomerCars { get; set; } = new List<CustomerCar>();
}