using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Services;

namespace AutoRepairCRM.Core.Contracts;

public interface IServiceService
{
    Task<IEnumerable<ActiveServiceViewModel>> GetActiveServices();

    Task<bool> CompleteService(ServiceCompleteModel model);

    Task<bool> Exists(int id);
}