using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
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

    public async Task<IEnumerable<ActiveServiceViewModel>> GetActiveServices()
    {
        return await _repo.AllReadonly<Service>()
            .Where(s => s.IsFinished == false)
            .Select(s => new ActiveServiceViewModel()
            {
                Id = s.Id,
                CarLicense = s.CustomerCar.LicensePlate,
                CustomerPhone = s.CustomerCar.Customer.User.PhoneNumber,
                DateStarted = s.DateStarted.ToString("dd-MM-yyyy"),
                EmployeeName = string.Join(", ", s.ServicesEmployees.Select(e => e.Employee.User!.FirstName)),
                Type = s.ServiceType.Name
            })
            .ToListAsync();
    }

    public async Task<bool> CompleteService(ServiceCompleteModel model)
    {
        var service = await _repo.GetByIdAsync<Service>(model.Id);
        
        service.DateEnded = DateTime.UtcNow;
        service.IsFinished = true;
        service.Price = model.Price;
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Exists(int id)
    {
        return await _repo.AllReadonly<Service>()
            .Where(s => s.Id == id)
            .AnyAsync();
    }
}