﻿using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Models.Account;

namespace AutoRepairCRM.Core.Contracts;

public interface IAccountService
{
    Task<ApplicationUser> CreateCustomer(CustomerInputModel model);
    
    Task<ApplicationUser> CreateEmployee(CustomerInputModel model, string position);
}