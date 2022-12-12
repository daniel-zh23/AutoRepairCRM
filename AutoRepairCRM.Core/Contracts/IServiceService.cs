using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Services;

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
}