using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class Service
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(ServiceType))]
    public int ServiceTypeId { get; set; }

    [Required]
    [Column(TypeName = DataConstants.Service.ServicePriceDecimal)]
    public decimal Price { get; set; }
    
    public int CustomerCarId { get; set; }

    [ForeignKey("CustomerId, CarId")] public CustomerCar CustomerCar { get; set; } = null!;

    public ServiceType ServiceType { get; set; } = null!;
}