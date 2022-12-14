using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.UnitTests.Services;

[TestFixture]
public class CarServiceTests
{
    private IRepository _repo = null!;
    private ICarService _carService = null!;
    private AutoRepairCrmDbContext _context = null!;

    [SetUp]
    public void Setup()
    {
        var contextOptions = new DbContextOptionsBuilder<AutoRepairCrmDbContext>()
            .UseInMemoryDatabase("AutoRepairCRM")
            .Options;

        _context = new AutoRepairCrmDbContext(contextOptions);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Test]
    public async Task GetAll_Should_Return_Correct_Amount_Of_Cars()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetAll("Bmw", 1, 4);
        
        Assert.That(result.Items.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetAll_Should_Return_Correct_Total_Cars()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetAll(null, 1, 3);
        
        Assert.That(result.Total, Is.EqualTo(6));
    }
    
    [Test]
    public async Task GetAllForCustomer_Should_Return_Correct_Total_Cars()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetAllForCustomer(1, 1, 2);
        
        Assert.That(result.TotalCars, Is.EqualTo(4));
    }
    
    [Test]
    public async Task GetAllForCustomer_Should_Return_Correct_Amount_Of_Cars()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetAllForCustomer(1, 1, 2);
        
        Assert.That(result.TotalCars, Is.EqualTo(4));
    }
    
    [Test]
    public async Task AddCar_Should_Return_Id_Of_New_Car()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.AddCar(new CarInputModel{Make = "Test", Model = "Test", Year = "2000 - 2000"});

        Assert.That(result, Is.EqualTo(7));
    }
    
    [Test]
    public async Task AddCar_Should_Add_Car_In_Database()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.AddCar(new CarInputModel{Make = "Test", Model = "Test", Year = "2000 - 2000"});

        var cars = await _repo.AllReadonly<Car>().CountAsync();
        
        Assert.That(cars, Is.EqualTo(7));
    }
    
    [Test]
    public async Task AddCar_Should_Return_Negative_One_On_Fail()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.AddCar(new CarInputModel{Make = null, Model = "Test", Year = "2000 - 2000"});
        
        Assert.That(result, Is.EqualTo(-1));
    }
    
    [Test]
    public async Task DeleteCar_Should_Return_True_On_Success()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.DeleteCar(1);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task DeleteCar_Should_Set_IsActive_To_False()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.DeleteCar(1);

        var car = await _repo.AllReadonly<Car>()
            .Where(c => c.Id == 1).FirstAsync();
        
        Assert.That(car.IsActive, Is.False);
    }
    
    [Test]
    public async Task DeleteCar_Should_Return_False_On_Fail()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.DeleteCar(10);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task GetAllCarsForForm_Should_Return_All_Cars()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetAllCarsForForm();
        
        Assert.That(result.Count(), Is.EqualTo(6));
    }
    
    [Test]
    public async Task CarExists_Should_Return_True_On_Found()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.CarExists(2);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task CarExists_Should_Return_False_On_Not_Found()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.CarExists(10000);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task GetFuelTypes_Should_Return_All_Fuel_Types()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.GetFuelTypes();
        
        Assert.That(result.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task FuelExists_Should_Return_True_On_Found()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.FuelExists(2);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task FuelExists_Should_Return_False_On_Not_Found()
    {
        _repo = new Repository(_context);
        _carService = new CarService(_repo);

        var result = await _carService.FuelExists(10000);
        
        Assert.That(result, Is.False);
    }
}