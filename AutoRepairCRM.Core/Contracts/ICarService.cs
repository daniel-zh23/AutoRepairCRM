using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Models;

namespace AutoRepairCRM.Core.Contracts;

public interface ICarService
{
    /// <summary>
    /// Gets call cars for customer
    /// </summary>
    /// <param name="customerId">Providing customerId to get his cars.</param>
    /// <param name="currPage">User for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>ForCustomerResultModel object.</returns>>
    Task<ForCustomerResultModel> GetAllForCustomer(int customerId, int currPage = 1, int perPage = 1);

    /// <summary>
    /// Gets all services info for customer's car
    /// </summary>
    /// <param name="carId">Providing carId to get his cars</param>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>CarDetails object.</returns>>
    Task<CarDetailsModel> GetServicesById(int carId, int customerId);

    /// <summary>
    /// Get all cars from the database
    /// </summary>
    /// <returns>IEnumerable from CarViewModel</returns>
    Task<IEnumerable<CarViewModel>> GetAllCarsAsync();

    /// <summary>
    /// Checks if car exists in the database
    /// </summary>
    /// <param name="carId">The id of the car you want to check</param>
    /// <returns>True if found, otherwise false.</returns>
    Task<bool> CarExists(int carId);

    /// <summary>
    /// Gets all fuel types from the database.
    /// </summary>
    /// <returns>IEnumerable of FuelType.</returns>
    Task<IEnumerable<FuelType>> GetFuelTypes();

    /// <summary>
    /// Checks if fuel exists in the database
    /// </summary>
    /// <param name="fuelId">Integer of the id to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    Task<bool> FuelExists(int fuelId);
    
    /// <summary>
    /// Checks if service type exists in the database
    /// </summary>
    /// <param name="serviceTypeId">Integer of the id to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    Task<bool> ServiceTypeExists(int serviceTypeId);
    
    /// <summary>
    /// Gets all service types from the database.
    /// </summary>
    /// <returns>IEnumerable of ServiceType.</returns>
    Task<IEnumerable<ServiceType>> GetServiceTypes();
}