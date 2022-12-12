using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Models.Account;

namespace AutoRepairCRM.Core.Contracts;

public interface ICustomerService
{
    /// <summary>
    /// Gets all customers depending on currPage and perPage parameters.
    /// </summary>
    /// <param name="searchTerm">Term to search in first name, last name or phone number.</param>
    /// <param name="currPage">Used for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>AllResultModel with total customers and collection of CustomerViewModel meting the criteria, if any.</returns>
    Task<AllResultModel<CustomerViewModel>> All(string? searchTerm = null, int currPage = 1, int perPage = 1);
    
    /// <summary>
    /// Gets customer id by giving ApplicationUser id
    /// </summary>
    /// <param name="userId">Providing ApplicationUser Id.</param>
    /// <returns>Customer id as int or -1 in case of not found</returns>>
    Task<int> GetCustomerId(string userId);

    /// <summary>
    /// Check if customer exists
    /// </summary>
    /// <param name="customerId">Providing CustomerId that we want to check</param>
    /// <returns>True if found, otherwise false.</returns>>
    Task<bool> Exists(int customerId);

    /// <summary>
    /// Adds customer in database.
    /// </summary>
    /// <param name="user">ApplicationUser that corresponds to that customer.</param>
    /// <returns>The id of the newly created customer.</returns>
    Task<int> Add(ApplicationUser user);
    
    /// <summary>
    /// Gets detailed customer information.
    /// </summary>
    /// <param name="id">Specifies customer id.</param>
    /// <returns>CustomerDetailsModel object.</returns>
    Task<CustomerDetailsModel> GetCustomerDetails(int id);

    /// <summary>
    /// Updates customer in the database.
    /// </summary>
    /// <param name="id">Id of the customer to update.</param>
    /// <param name="model">CustomerInputModel with new parameters.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> Update(int id, CustomerInputModel model);

    /// <summary>
    /// Adds new car to a customer.
    /// </summary>
    /// <param name="model">CustomerCarInputModel object.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> AddCustomerCar(CustomerCarInputModel model);
    
    /// <summary>
    /// Gets the customer car to be edited by composite key.
    /// </summary>
    /// <param name="carId">CarId part of the composite key.</param>
    /// <param name="customerId">CustomerId part of the composite key.</param>
    /// <returns>CustomerCarInputModel object.</returns>
    Task<CustomerCarInputModel> GetCustomerCar(int carId, int customerId);
    
    /// <summary>
    /// Updates customer car in the database.
    /// </summary>
    /// <param name="model">CustomerCarInputModel with new parameters.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> UpdateCustomerCar(CustomerCarInputModel model);
    
    /// <summary>
    /// Adds new service to existing customer's car.
    /// </summary>
    /// <param name="model">CarServiceInputModel object.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> AddCustomerCarService(CarServiceInputModel model);
}