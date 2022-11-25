using AutoRepairCRM.Core.Models;

namespace AutoRepairCRM.Models;

public class AllForCustomerModel
{
    public int TotalCars { get; set; }

    public const int CarsPerPage = 5;

    public int CurrentPage { get; set; } = 1;

    public IEnumerable<CustomerCarViewModel> Cars { get; set; } = Enumerable.Empty<CustomerCarViewModel>();
}