namespace AutoRepairCRM.Core.Models.Customer;

public class ForCustomerResultModel
{
    public int TotalCars { get; set; }

    public IEnumerable<CustomerCarViewModel> Cars { get; set; } = new List<CustomerCarViewModel>();
}