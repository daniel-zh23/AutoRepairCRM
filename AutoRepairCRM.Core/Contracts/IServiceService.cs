using AutoRepairCRM.Core.Models;

namespace AutoRepairCRM.Core.Contracts;

public interface IServiceService
{
    Task<IEnumerable<ActiveServiceViewModel>> GetActiveServices();
}