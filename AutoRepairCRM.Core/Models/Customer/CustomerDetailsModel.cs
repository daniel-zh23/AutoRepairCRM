namespace AutoRepairCRM.Core.Models.Customer;

public class CustomerDetailsModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public IEnumerable<CustomerCarViewModel> Cars = new List<CustomerCarViewModel>();
}