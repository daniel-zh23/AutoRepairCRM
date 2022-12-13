using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository _repo;

    public CustomerService(IRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Gets all customers depending on currPage and perPage parameters.
    /// </summary>
    /// <param name="searchTerm">Term to search in first name, last name or phone number.</param>
    /// <param name="currPage">User for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>AllResultModel with total customers and collection of CustomerViewModel meting the criteria, if any.</returns>
    public async Task<AllResultModel<CustomerViewModel>> All(string? searchTerm = null, int currPage = 1, int perPage = 1)
    {
        var result = new AllResultModel<CustomerViewModel>();
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

        result.Items =  await customers
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Phone = c.User.PhoneNumber
            }).ToListAsync();
        result.Total = await customers.CountAsync();

        return result;
    }

    /// <summary>
    /// Gets customer id by giving ApplicationUser id
    /// </summary>
    /// <param name="userId">Providing ApplicationUser Id.</param>
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
    /// <returns>True if found, otherwise false.</returns>>
    public async Task<bool> Exists(int customerId)
    {
        var customer = await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == customerId)
            .FirstOrDefaultAsync();

        return customer != null;
    }

    /// <summary>
    /// Adds customer in database.
    /// </summary>
    /// <param name="user">ApplicationUser that corresponds to that customer.</param>
    /// <returns>The id of the newly created customer.</returns>
    public async Task<int> Add(ApplicationUser user)
    {
        var customer = new Customer
        {
            User = user
        };
        try
        {
            await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();
        }
        catch (Exception)
        {
            return -1;
        }
        
        return customer.Id;
    }

    /// <summary>
    /// Gets detailed customer information.
    /// </summary>
    /// <param name="id">Specifies customer id.</param>
    /// <returns>CustomerDetailsModel object.</returns>
    public async Task<CustomerDetailsModel> GetCustomerDetails(int id)
    {
        return await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == id)
            .Select(c => new CustomerDetailsModel
            {
                Id = c.Id,
                Email = c.User.Email,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Phone = c.User.PhoneNumber,
                Cars = c.CustomerCars.Select(cc => new CustomerCarViewModel
                {
                    CarId = cc.CarId,
                    CustomerId = cc.CustomerId,
                    FuelType = cc.FuelType.Name,
                    LicensePlate = cc.LicensePlate,
                    Litre = cc.EngineLitre,
                    Make = cc.Car.Make,
                    Model = cc.Car.Model,
                    Year = cc.Car.Year
                })
            })
            .FirstAsync();
    }

    /// <summary>
    /// Updates customer in the database.
    /// </summary>
    /// <param name="id">Id of the customer to update.</param>
    /// <param name="model">CustomerInputModel with new parameters.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> Update(int id, CustomerInputModel model)
    {
        var customer = await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == id)
            .FirstAsync();
        var user = await _repo.GetByIdAsync<ApplicationUser>(customer.UserId);
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.Phone;
        user.Email = model.Email;

        await _repo.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Adds new car to a customer.
    /// </summary>
    /// <param name="model">CustomerCarInputModel object.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> AddCustomerCar(CustomerCarInputModel model)
    {
        await _repo.AddAsync(new CustomerCar
        {
            CarId = model.CarId,
            CustomerId = model.CustomerId,
            EngineLitre = model.Litre,
            FuelTypeId = model.FuelTypeId,
            LicensePlate = model.LicensePlate
        });
        
        await _repo.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Gets the customer car to be edited by composite key.
    /// </summary>
    /// <param name="carId">CarId part of the composite key.</param>
    /// <param name="customerId">CustomerId part of the composite key.</param>
    /// <returns>CustomerCarInputModel object.</returns>
    public async Task<CustomerCarInputModel> GetCustomerCar(int carId, int customerId)
    {
        return await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CarId == carId && cc.CustomerId == customerId)
            .Select(cc => new CustomerCarInputModel
            {
                CarId = cc.CarId,
                CustomerId = cc.CustomerId,
                FuelTypeId = cc.FuelTypeId,
                LicensePlate = cc.LicensePlate,
                Litre = cc.EngineLitre
            })
            .FirstAsync();
    }

    /// <summary>
    /// Updates customer car in the database.
    /// </summary>
    /// <param name="model">CustomerCarInputModel with new parameters.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> UpdateCustomerCar(CustomerCarInputModel model)
    {
        var customerCar = await _repo.GetByIdsAsync<CustomerCar>(new object[] {model.CarId, model.CustomerId});
        customerCar.FuelTypeId = model.FuelTypeId;
        customerCar.EngineLitre = model.Litre;
        customerCar.LicensePlate = model.LicensePlate;
        customerCar.CarId = model.CarId;

        await _repo.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Adds new service to existing customer's car.
    /// </summary>
    /// <param name="model">CarServiceInputModel object.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> AddCustomerCarService(CarServiceInputModel model)
    {
        var service = new Service
        {
            CarId = model.CarId,
            CustomerId = model.CustomerId,
            DateStarted = model.DateStarted,
            IsFinished = false,
            ServiceTypeId = model.ServiceTypeId
        };

        var services = model.Employees.Select(emp => new ServiceEmployee { EmployeeId = emp, Service = service }).ToList();

        await _repo.AddRangeAsync(services);
        await _repo.AddAsync(service);
        await _repo.SaveChangesAsync();
        return true;
    }
}