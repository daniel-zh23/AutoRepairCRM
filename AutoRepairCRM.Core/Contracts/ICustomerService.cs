using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Customer;

namespace AutoRepairCRM.Core.Contracts;

public interface ICustomerService
{
    Task<CustomerResultModel> All(string? searchTerm = null, int currPage = 1, int perPage = 1);
    
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

    /// <summary>
    /// Adds customer in database and creates it's account
    /// </summary>
    /// <param name="model">CustomerInput model to create the user and the customer</param>
    /// <returns>True or false if completed successfully</returns>
    Task<bool> Add(CustomerInputModel model);
}