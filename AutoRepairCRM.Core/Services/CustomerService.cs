using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class CustomerService : ICustomerService
{
    private IRepository _repo;
    private UserManager<ApplicationUser> _userManager;

    public CustomerService(IRepository repo, UserManager<ApplicationUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    public async Task<CustomerResultModel> All(string? searchTerm = null, int currPage = 1, int perPage = 1)
    {
        var result = new CustomerResultModel();
        var customers = _repo.AllReadonly<Customer>();
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = $"%{searchTerm.ToLower()}%";

            customers = customers
                .Include(c => c.User)
                .Where(c => EF.Functions.Like(c.User.FirstName, searchTerm) ||
                                 EF.Functions.Like(c.User.LastName, searchTerm) ||
                                 EF.Functions.Like(c.User.PhoneNumber, searchTerm));
        }

        result.Customers =  await customers
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(c => new CustomerViewModel()
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Phone = c.User.PhoneNumber
            }).ToListAsync();
        result.TotalCustomers = await customers.CountAsync();

        return result;
    }

    /// <summary>
    /// Gets customer id
    /// </summary>
    /// <param name="userId">Providing ApplicationUserId to get the correct customer</param>
    /// <returns>Customer id as int or -1 in case of not found</returns>>
    public async Task<int> GetCustomerId(string userId)
    {
        return await _repo.AllReadonly<Customer>(c => c.UserId == userId)
            .Select(c => c.Id)
            .FirstAsync();
    }

    /// <summary>
    /// Check if customer exists
    /// </summary>
    /// <param name="customerId">Providing CustomerId that we want to check</param>
    /// <returns>True or false</returns>>
    public async Task<bool> Exists(int customerId)
    {
        var customer = await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == customerId)
            .FirstOrDefaultAsync();

        return customer != null;
    }

    /// <summary>
    /// Adds customer in database and creates it's account
    /// </summary>
    /// <param name="model">CustomerInput model to create the user and the customer</param>
    /// <returns>True or false if completed successfully</returns>
    public async Task<bool> Add(CustomerInputModel model)
    {
        var user = new ApplicationUser()
        {
            UserName = $"{model.FirstName} {model.LastName}",
            NormalizedUserName = $"{model.FirstName} {model.LastName}".ToUpper(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            PhoneNumber = model.Phone
        };
        
        var result = await _userManager.CreateAsync(user, $"{model.FirstName}{model.Phone}.");
        if (!result.Succeeded)
        {
            return false;
        }
        await _userManager.AddToRoleAsync(user, "Customer");
        var customer = new Customer()
        {
            User = user
        };

        await _repo.AddAsync(customer);
        await _repo.SaveChangesAsync();
        
        return true;
    }
}