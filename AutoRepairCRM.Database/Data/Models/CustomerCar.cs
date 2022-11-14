using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRepairCRM.Database.Data.Models;

public class CustomerCar
{
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    [ForeignKey(nameof(Car))]
    public int CarId { get; set; }

    [Required] public string LicensePlate { get; set; } = null!;

    public IEnumerable<Service> Services { get; set; } = new List<Service>();

    public Customer Customer { get; set; } = null!;

    public Car Car { get; set; } = null!;
}