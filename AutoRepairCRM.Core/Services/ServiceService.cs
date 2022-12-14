using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class ServiceService : IServiceService
{
    private readonly IRepository _repo;

    public ServiceService(IRepository repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Gets all services that are not completed.
    /// </summary>
    /// <returns>IEnumerable of ActiveServiceViewModel objects.</returns>
    public async Task<IEnumerable<ActiveServiceViewModel>> GetActiveServices()
    {
        return await _repo.AllReadonly<Service>()
            .Where(s => s.IsFinished == false)
            .Select(s => new ActiveServiceViewModel
            {
                Id = s.Id,
                CarLicense = s.CustomerCar.LicensePlate,
                CustomerPhone = s.CustomerCar.Customer.User.PhoneNumber,
                DateStarted = s.DateStarted.ToString("dd-MM-yyyy"),
                EmployeeName = string.Join(", ", s.ServicesEmployees.Select(e => 
                    e.Employee.User == null ? "" : e.Employee.User.FirstName)),
                Type = s.ServiceType.Name
            })
            .ToListAsync();
    }

    /// <summary>
    /// Completes a certain service.
    /// </summary>
    /// <param name="model">ServiceCompleteModel with the price of the service.</param>
    /// <returns>True if completed, otherwise false.</returns>
    public async Task<bool> CompleteService(ServiceCompleteModel model)
    {
        var service = await _repo.GetByIdAsync<Service>(model.Id);
        
        service.DateEnded = DateTime.UtcNow;
        service.IsFinished = true;
        service.Price = model.Price;
        await _repo.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Checks if service exists in the database.
    /// </summary>
    /// <param name="id">Id of service to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    public async Task<bool> Exists(int id)
    {
        return await _repo.AllReadonly<Service>()
            .Where(s => s.Id == id)
            .AnyAsync();
    }

    /// <summary>
    /// Checks if service type exists in the database
    /// </summary>
    /// <param name="serviceTypeId">Integer of the id to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    public async Task<bool> ServiceTypeExists(int serviceTypeId)
    {
        return await _repo.AllReadonly<ServiceType>()
            .AnyAsync(f => f.Id == serviceTypeId);
    }

    /// <summary>
    /// Gets all service types from the database.
    /// </summary>
    /// <returns>IEnumerable of ServiceType.</returns>
    public async Task<IEnumerable<ServiceType>> GetServiceTypes()
    {
        return await _repo.AllReadonly<ServiceType>().ToListAsync();
    }

    /// <summary>
    /// Gets all services info for customer's car
    /// </summary>
    /// <param name="carId">Providing carId to get his cars</param>
    /// <param name="customerId">Providing customerId to get his cars</param>
    /// <returns>CarDetails object.</returns>>
    public async Task<CarDetailsModel> GetServicesById(int carId, int customerId)
    {
        return await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CustomerId == customerId && cc.CarId == carId)
            .Select(cc => new CarDetailsModel
            {
                Make = cc.Car.Make,
                Model = cc.Car.Model,
                Services = cc.Services
                    .Select(s => new CustomerServiceViewModel
                    {
                        ServiceType = s.ServiceType.Name,
                        ServiceState = s.IsFinished,
                        StartDate = s.DateStarted.ToString("dd-MM-yyyy"),
                        EndDate = s.DateEnded == null ? "" : s.DateEnded.Value.ToString("dd-MM-yyyy"),
                        Price = s.Price
                    })
            }).FirstAsync();
    }
}