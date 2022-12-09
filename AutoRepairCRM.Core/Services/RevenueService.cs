using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Revenue;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class RevenueService : IRevenueService
{
    private readonly IRepository _repo;

    public RevenueService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<RevenueModel> GetRevenue()
    {
        var result = new RevenueModel();
        result.RevenueThisMonth = await _repo.AllReadonly<Service>()
            .Select(s => s.Price).SumAsync();

        result.
        
        return result;
    }
}