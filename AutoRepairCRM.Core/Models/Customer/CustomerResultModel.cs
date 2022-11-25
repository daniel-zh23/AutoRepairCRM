namespace AutoRepairCRM.Core.Models.Customer;

public class CustomerResultModel
{
    public int TotalCustomers { get; set; }

    public IEnumerable<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
}