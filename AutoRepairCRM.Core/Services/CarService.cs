using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
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

    public async Task<ForCustomerResultModel> GetAllForCustomer(int customerId, int currPage = 1, int perPage = 1)
    {
        var result = new ForCustomerResultModel();
        var cars = _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CustomerId == customerId);

        result.TotalCars = await cars.CountAsync();
        result.Cars = await cars
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Include(c => c.Car)
            .Include(c => c.FuelType)
            .Select(c => new CustomerCarViewModel()
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

        return result;
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
                        StartDate = s.DateStarted.ToString("dd-MM-yyyy"),
                        EndDate = s.DateEnded == null ? "" : s.DateEnded.Value.ToString("dd-MM-yyyy"),
                        Price = s.Price
                    })
            }).FirstAsync();
    }

    public async Task<IEnumerable<CarViewModel>> GetAllCarsAsync()
    {
        return await _repo.AllReadonly<Car>()
            .Select(c => new CarViewModel()
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year
            }).ToListAsync();
    }

    public async Task<bool> CarExists(int carId)
    {
        return await _repo.AllReadonly<Car>()
            .AnyAsync(c => c.Id == carId);
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _repo.AllReadonly<FuelType>().ToListAsync();
    }

    public async Task<bool> FuelExists(int fuelId)
    {
        return await _repo.AllReadonly<FuelType>()
            .AnyAsync(f => f.Id == fuelId);
    }

    public async Task<bool> ServiceTypeExists(int fuelId)
    {
        return await _repo.AllReadonly<ServiceType>()
            .AnyAsync(f => f.Id == fuelId);
    }

    public async Task<IEnumerable<ServiceType>> GetServiceTypes()
    {
        return await _repo.AllReadonly<ServiceType>().ToListAsync();
    }
}