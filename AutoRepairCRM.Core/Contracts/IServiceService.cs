using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Database.Data.Models;

namespace AutoRepairCRM.Core.Contracts;

public interface IServiceService
{
    /// <summary>
    /// Gets all services that are not completed.
    /// </summary>
    /// <returns>IEnumerable of ActiveServiceViewModel objects.</returns>
    Task<IEnumerable<ActiveServiceViewModel>> GetActiveServices();

    /// <summary>
    /// Completes a certain service.
    /// </summary>
    /// <param name="model">ServiceCompleteModel with the price of the service.</param>
    /// <returns>True if completed, otherwise false.</returns>
    Task<bool> CompleteService(ServiceCompleteModel model);

    /// <summary>
    /// Checks if service exists in the database.
    /// </summary>
    /// <param name="id">Id of service to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    Task<bool> Exists(int id);
    
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
    
    /// <summary>
    /// Gets all services info for customer's car
    /// </summary>
    /// <param name="carId">Providing carId to get his cars</param>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>CarDetailsModel object.</returns>>
    Task<CarDetailsModel> GetServicesById(int carId, int customerId);
}