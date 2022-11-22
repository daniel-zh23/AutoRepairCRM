using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class CustomerService : ICustomerService
{
    private IRepository _repo;

    public CustomerService(IRepository repo)
    {
        _repo = repo;
    }
    public async Task<int> GetCustomerId(string userId)
    {
        return await _repo.AllReadonly<Customer>(c => c.UserId == userId)
            .Select(c => c.Id)
            .FirstAsync();
    }

    public async Task<bool> Exists(int customerId)
    {
        var customer = await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == customerId)
            .FirstOrDefaultAsync();

        return customer != null;
    }
}