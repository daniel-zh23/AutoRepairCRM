using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class CarService : ICarService
{
    private IRepository _repo;

    public CarService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CarViewModel>> GetAllForCustomer(int customerId)
    {
        return await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CustomerId == customerId)
            .Include(c => c.Car)
            .Include(c => c.FuelType)
            .Select(c => new CarViewModel()
            {
                CarId = c.CarId,
                CustomerId = c.CustomerId,
                Make = c.Car.Make,
                Model = c.Car.Model,
                LicensePlate = c.LicensePlate,
                Year = c.Car.Year,
                Litre = c.EngineLitre,
                FuelType = c.FuelType.Name
            }).ToListAsync();
    }

    public async Task<CarDetailsModel> GetServicesById(int carId, int customerId)
    {
        return await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CustomerId == customerId && cc.CarId == carId)
            .Select(cc => new CarDetailsModel()
            {
                Make = cc.Car.Make,
                Model = cc.Car.Model,
                Services = cc.Services
                    .Select(s => new ServiceViewModel()
                    {
                        ServiceType = s.ServiceType.Name,
                        ServiceState = s.IsFinished,
                        StartDate = s.DateStarted,
                        EndDate = s.DateEnded,
                        Price = s.Price
                    })
            }).FirstAsync();
    }
}