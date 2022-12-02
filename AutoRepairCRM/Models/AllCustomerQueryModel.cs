using AutoRepairCRM.Core.Models;

namespace AutoRepairCRM.Models;

public class AllCustomerQueryModel
{
    public const int CustomersPerPage = 5;

    public string? SearchTerm { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int TotalCustomers { get; set; }

    public IEnumerable<CustomerViewModel> Customers { get; set; } = Enumerable.Empty<CustomerViewModel>();
}