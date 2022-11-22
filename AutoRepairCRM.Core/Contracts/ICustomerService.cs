namespace AutoRepairCRM.Core.Contracts;

public interface ICustomerService
{
    /// <summary>
    /// Gets customer id
    /// </summary>
    /// <param name="userId">Providing ApplicationUserId to get the correct customer</param>
    /// <returns>Customer id as int or -1 in case of not found</returns>>
    Task<int> GetCustomerId(string userId);

    /// <summary>
    /// Check if customer exists
    /// </summary>
    /// <param name="customerId">Providing CustomerId that we want to check</param>
    /// <returns>True or false</returns>>
    Task<bool> Exists(int customerId);
}