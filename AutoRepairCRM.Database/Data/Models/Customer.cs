using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRepairCRM.Database.Data.Constants;
using AutoRepairCRM.Database.Data.Models.Account;

namespace AutoRepairCRM.Database.Data.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(User))] 
    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public IEnumerable<CustomerCar> CustomerCars { get; set; } = new List<CustomerCar>();
}