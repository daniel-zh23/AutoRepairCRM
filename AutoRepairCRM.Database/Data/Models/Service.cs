using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoRepairCRM.Database.Data.Constants;

namespace AutoRepairCRM.Database.Data.Models;

public class Service
{
    [Key]
    public int Id { get; set; }

    [Required]
    public bool IsFinished { get; set; }

    [ForeignKey(nameof(ServiceType))]
    public int ServiceTypeId { get; set; }
    
    [Required]
    public int CarId { get; set; }
    
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public DateTime DateStarted { get; set; }
    
    public DateTime? DateEnded { get; set; }
    
    [Column(TypeName = DataConstants.Service.ServicePriceDecimal)]
    public decimal? Price { get; set; }

    [ForeignKey($"{nameof(CarId)}, {nameof(CustomerId)}")]
    public CustomerCar CustomerCar { get; set; } = null!;

    public ServiceType ServiceType { get; set; } = null!;
    
    
    public IEnumerable<ServiceEmployee> ServicesEmployees { get; set; } = new List<ServiceEmployee>();
}