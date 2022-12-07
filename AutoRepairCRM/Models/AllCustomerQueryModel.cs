using AutoRepairCRM.Core.Models;

namespace AutoRepairCRM.Models;

public class AllCustomerQueryModel
{
    public const int PeoplePerPage = 5;

    public string? SearchTerm { get; set; }

    public int CurrentPage { get; set; } = 1;

    public int Total { get; set; }

    public IEnumerable<CustomerViewModel> People { get; set; } = Enumerable.Empty<CustomerViewModel>();
}