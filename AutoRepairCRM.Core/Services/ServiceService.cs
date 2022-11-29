using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
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
                EmployeeName = string.Join(", ", s.ServicesEmployees.Select(e => e.Employee.FirstName)),
                Type = s.ServiceType.Name
            })
            .ToListAsync();
    }
}