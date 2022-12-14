using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Core.Services;

public class CarService : ICarService
{
    private readonly IRepository _repo;

    public CarService(IRepository repo)
    {
        _repo = repo;
    }

    public async Task<AllResultModel<CarViewModel>> GetAll(string? searchTerm, int currPage = 1, int perPage = 1)
    {
        var result = new AllResultModel<CarViewModel>();
        var cars = _repo.AllReadonly<Car>()
            .Where(c => c.IsActive == true);
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = $"%{searchTerm.ToLower()}%";

            cars = cars
                .Where(c => EF.Functions.Like(c.Make, searchTerm) ||
                            EF.Functions.Like(c.Model, searchTerm) ||
                            EF.Functions.Like(c.Year, searchTerm));
        }

        result.Items = await cars
            .Skip((currPage - 1) * perPage)
            .Take(perPage)
            .Select(c => new CarViewModel()
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year
            }).ToListAsync();
        
        result.Total = await cars.CountAsync();

        return result;
    }

    /// <summary>
    /// Gets call cars for customer
    /// </summary>
    /// <param name="customerId">Providing customerId to get his cars.</param>
    /// <param name="currPage">User for pagination.</param>
    /// <param name="perPage">Specifies how many to get from te database.</param>
    /// <returns>ForCustomerResultModel object.</returns>>
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
            .Select(c => new CustomerCarViewModel
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

    /// <summary>
    /// Adds a car to the database.
    /// </summary>
    /// <param name="model">CarInputModel object.</param>
    /// <returns>The id of newly created car, -1 if not created.</returns>
    public async Task<int> AddCar(CarInputModel model)
    {
        var car = new Car
        {
            Make = model.Make,
            Model = model.Model,
            Year = model.Year,
            IsActive = true
        };

        try
        {
            await _repo.AddAsync(car);
            await _repo.SaveChangesAsync();
        }
        catch (Exception)
        {
            return -1;
        }
        
        return car.Id;
    }

    /// <summary>
    /// Changes active status of a car.
    /// </summary>
    /// <param name="id">Id of the car to remove.</param>
    /// <returns>True if successful, otherwise false.</returns>
    public async Task<bool> DeleteCar(int id)
    {
        var car = await _repo.GetByIdAsync<Car>(id);

        if (car == null)
            return false;

        car.IsActive = false;
        await _repo.SaveChangesAsync();
        
        return true;
    }

    /// <summary>
    /// Get all cars from the database
    /// </summary>
    /// <returns>IEnumerable from CarViewModel</returns>
    public async Task<IEnumerable<CarViewModel>> GetAllCarsForForm()
    {
        return await _repo.AllReadonly<Car>()
            .Where(c => c.IsActive == true)
            .Select(c => new CarViewModel
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year
            }).ToListAsync();
    }

    /// <summary>
    /// Checks if car exists in the database
    /// </summary>
    /// <param name="carId">The id of the car you want to check</param>
    /// <returns>True if found, otherwise false.</returns>
    public async Task<bool> CarExists(int carId)
    {
        return await _repo.AllReadonly<Car>()
            .Where(c => c.IsActive)
            .AnyAsync(c => c.Id == carId);
    }

    /// <summary>
    /// Gets all fuel types from the database.
    /// </summary>
    /// <returns>IEnumerable of FuelType.</returns>
    public async Task<IEnumerable<FuelType>> GetFuelTypes()
    {
        return await _repo.AllReadonly<FuelType>().ToListAsync();
    }

    /// <summary>
    /// Checks if fuel exists in the database
    /// </summary>
    /// <param name="fuelId">Integer of the id to check.</param>
    /// <returns>True if found, otherwise false.</returns>
    public async Task<bool> FuelExists(int fuelId)
    {
        return await _repo.AllReadonly<FuelType>()
            .AnyAsync(f => f.Id == fuelId);
    }
}