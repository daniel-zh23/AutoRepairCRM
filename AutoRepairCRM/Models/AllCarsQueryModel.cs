using AutoRepairCRM.Core.Models.Car;

namespace AutoRepairCRM.Models;

public class AllCarsQueryModel
{
    public const int CarsPerPage = 5;

    public string? SearchTerm { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int Total { get; set; }

    public IEnumerable<CarViewModel> Cars { get; set; } = Enumerable.Empty<CarViewModel>();
}