using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Database.Data.Models;

namespace AutoRepairCRM.Core.Contracts;

public interface ICarService
{
    /// <summary>
    /// Gets call cars for customer
    /// </summary>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>IQueryable of CarViewModel</returns>>
    Task<IEnumerable<CarViewModel>> GetAllForCustomer(int customerId);

    /// <summary>
    /// Gets all services info for customer's car
    /// </summary>
    /// <param name="carId">Providing carId to get his cars</param>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>IQueryable of CarDetailsModel</returns>>
    Task<CarDetailsModel> GetServicesById(int carId, int customerId);
}