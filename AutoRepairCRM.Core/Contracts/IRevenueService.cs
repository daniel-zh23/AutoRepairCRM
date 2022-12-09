using AutoRepairCRM.Core.Models.Revenue;

namespace AutoRepairCRM.Core.Contracts;

public interface IRevenueService
{
    Task<RevenueModel> GetRevenue();
}