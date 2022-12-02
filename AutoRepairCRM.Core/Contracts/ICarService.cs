using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Models;

namespace AutoRepairCRM.Core.Contracts;

public interface ICarService
{
    /// <summary>
    /// Gets call cars for customer
    /// </summary>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <param name="currPage"></param>
    /// <param name="perPage"></param>
    /// <returns>IQueryable of CustomerCarViewModel</returns>>
    Task<ForCustomerResultModel> GetAllForCustomer(int customerId, int currPage = 1, int perPage = 1);

    /// <summary>
    /// Gets all services info for customer's car
    /// </summary>
    /// <param name="carId">Providing carId to get his cars</param>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>IQueryable of CarDetailsModel</returns>>
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
    /// <returns>Boolean true or false</returns>
    Task<bool> CarExists(int carId);

    Task<IEnumerable<FuelType>> GetFuelTypes();

    Task<bool> FuelExists(int fuelId);
    
    Task<bool> ServiceTypeExists(int fuelId);
    
    Task<IEnumerable<ServiceType>> GetServiceTypes();
}