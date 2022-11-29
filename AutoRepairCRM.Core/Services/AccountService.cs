using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Core.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public AccountService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> CreateCustomer(CustomerInputModel model)
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
            throw new ArgumentException("Error creating customer account!");
        }
        await _userManager.AddToRoleAsync(user, "Customer");

        return user;
    }

    public Task<ApplicationUser> CreateEmployee(CustomerInputModel model, string position)
    {
        throw new NotImplementedException();
    }
}