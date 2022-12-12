﻿using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace AutoRepairCRM.Core.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRepository _repo;
    
    public AccountService(UserManager<ApplicationUser> userManager, IRepository repo)
    {
        _userManager = userManager;
        _repo = repo;
    }

    /// <summary>
    /// Creates a user for a registered customer.
    /// </summary>
    /// <param name="model">CustomerInputModel object.</param>
    /// <returns>ApplicationUser object created by the model with password generated by the first name phone number and '.' at the end.</returns>
    public async Task<ApplicationUser?> CreateCustomer(CustomerInputModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
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
            return null;
        }
        await _userManager.AddToRoleAsync(user, "Customer");

        return user;
    }

    /// <summary>
    /// Creates a user for a registered employee.
    /// </summary>
    /// <param name="model">EmployeeInputModel object.</param>
    /// <returns>ApplicationUser object created by the model with password generated by the first name phone number and '.' at the end.</returns>
    public async Task<ApplicationUser?> CreateEmployee(EmployeeInputModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            NormalizedUserName = $"{model.FirstName} {model.LastName}".ToUpper(),
            FirstName = model.FirstName,
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            LastName = model.LastName,
            PhoneNumber = model.Phone
        };
        
        var result = await _userManager.CreateAsync(user, $"{model.FirstName}{model.Phone}.");
        if (!result.Succeeded)
        {
            return null;
        }


        await _userManager.AddToRoleAsync(user, model.Roles);


        return user;
    }

    /// <summary>
    /// Gets all user roles.
    /// </summary>
    /// <param name="user">ApplicationUser object.</param>
    /// <returns>IEnumerable of strings. Each string is one role.</returns>
    public async Task<IEnumerable<string>> GetRolesForUser(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    /// <summary>
    /// Makes ApplicationUser account inactive and inaccessible.
    /// </summary>
    /// <param name="id">String the id of the user.</param>
    /// <returns>True if deactivated successfully, otherwise false.</returns>
    public async Task<bool> Deactivate(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        user.IsActive = false;
        return true;
    }
}