using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Car;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.UnitTests.Services;

public class CustomerServiceTests
{
    private IRepository _repo = null!;
    private ICustomerService _customerService = null!;
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

        _repo = new Repository(_context);
        _customerService = new CustomerService(_repo);
    }

    [TestCase("Ivan", 2)]
    [TestCase(null, 5)]
    public async Task All_Should_Return_Correct_Amount_Of_Customers(string name, int count)
    {
        var result = await _customerService.All(name, 1, 10);

        Assert.That(result.Items.Count(), Is.EqualTo(count));
    }

    [Test]
    public async Task All_Should_Return_Correct_Correct_Total_Customers()
    {
        var result = await _customerService.All(null, 1, 10);

        Assert.That(result.Items.Count(), Is.EqualTo(5));
    }

    [TestCase("MockUser5", 2)]
    [TestCase("MockUser6", 3)]
    [TestCase("MockUser2", 1)]
    public async Task GetCustomerId_Should_Return_Id_Associated_With_Provided_UserId(string userId, int customerId)
    {
        var result = await _customerService.GetCustomerId(userId);

        Assert.That(result, Is.EqualTo(customerId));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task Exists_Should_Return_True_When_Found(int customerId)
    {
        var result = await _customerService.Exists(customerId);

        Assert.That(result, Is.True);
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(100000)]
    public async Task Exists_Should_Return_False_When_Not_Found(int customerId)
    {
        var result = await _customerService.Exists(customerId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Add_Should_Return_Id_Of_New_Customer()
    {
        var user = new ApplicationUser()
        {
            Id = "TestId",
            UserName = "test@abv.bg",
            FirstName = "Test",
            LastName = "Test",
            NormalizedUserName = "TEST@ABV.BG",
            Email = "test@abv.bg",
            NormalizedEmail = "TEST@ABV.BG",
            PhoneNumber = "00000",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        
        var result = await _customerService.Add(user);

        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public async Task Add_Should_Add_Customer_In_Database()
    {
        var user = new ApplicationUser()
        {
            Id = "TestId",
            UserName = "test@abv.bg",
            FirstName = "Test",
            LastName = "Test",
            NormalizedUserName = "TEST@ABV.BG",
            Email = "test@abv.bg",
            NormalizedEmail = "TEST@ABV.BG",
            PhoneNumber = "00000",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        
        await _customerService.Add(user);

        var result = await _repo.AllReadonly<Customer>().CountAsync();

        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public async Task Add_Should_Return_Negative_One_On_Fail()
    {
        var result = await _customerService.Add(null!);

        Assert.That(result, Is.EqualTo(-1));
    }
    
    [Test]
    public async Task GetCustomerDetails_Should_Return_Correct_Customer_Details()
    {
        var result = await _customerService.GetCustomerDetails(1);

        Assert.That(result.Id, Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetCustomerDetails_Should_Return_Correct_Car_Count()
    {
        var result = await _customerService.GetCustomerDetails(1);

        Assert.That(result.Cars.Count(), Is.EqualTo(4));
    }
    
    [Test]
    public async Task Update_Should_Return_True_When_Completed()
    {
        var model = new CustomerInputModel()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            Phone = ""
        };
        
        var result = await _customerService.Update(1, model);

        Assert.That(result, Is.True);
    }
    
    [TestCase(1)]
    [TestCase(-10)]
    public async Task Update_Should_Return_False_When_Failed(int id)
    {
        var result = await _customerService.Update(id, null!);

        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task Update_Should_Change_Values_In_Database()
    {
        var model = new CustomerInputModel()
        {
            FirstName = "Test",
            LastName = "",
            Email = "",
            Phone = ""
        };
        
        await _customerService.Update(1, model);
        var customer = await _repo.AllReadonly<Customer>()
            .Where(c => c.Id == 1)
            .Select(c => c.User).FirstAsync();
        
        Assert.That(customer.FirstName, Is.EqualTo("Test"));
    }
    
    [Test]
    public async Task AddCustomerCar_Should_Return_True_When_Completed()
    {
        var model = new CustomerCarInputModel()
        {
            CarId = 1,
            CustomerId = 2, 
            Litre = "",
            FuelTypeId = 2,
            LicensePlate = ""
        };
        
        var result = await _customerService.AddCustomerCar(model);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task AddCustomerCar_Should_Return_False_When_Failed()
    {
        var result = await _customerService.AddCustomerCar(null!);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task AddCustomerCar_Should_Add_Car_To_Customer()
    {
        var model = new CustomerCarInputModel()
        {
            CarId = 1,
            CustomerId = 2, 
            Litre = "10",
            FuelTypeId = 2,
            LicensePlate = "Test5"
        };
        
        await _customerService.AddCustomerCar(model);

        var result = await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CarId == 1 && cc.CustomerId == 2).CountAsync();
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetCustomerCar_Should_Return_Correct_Data()
    {
        var result = await _customerService.GetCustomerCar(2, 1);

        Assert.That(result.LicensePlate, Is.EqualTo("В 5487 СМ"));
    }
    
    [Test]
    public async Task UpdateCustomerCar_Should_Return_True_When_Completed()
    {
        var model = new CustomerCarInputModel()
        {
            CarId = 2,
            CustomerId = 1, 
            Litre = "",
            FuelTypeId = 2,
            LicensePlate = ""
        };
        
        var result = await _customerService.UpdateCustomerCar(model);

        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task UpdateCustomerCar_Should_Return_False_When_Failed()
    {
        var result = await _customerService.UpdateCustomerCar(null!);

        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task UpdateCustomerCar_Should_Update_Data_In_Database()
    {
        var model = new CustomerCarInputModel()
        {
            CarId = 2,
            CustomerId = 1, 
            Litre = "10L",
            FuelTypeId = 2,
            LicensePlate = ""
        };
        
        await _customerService.UpdateCustomerCar(model);
        var result = await _repo.AllReadonly<CustomerCar>()
            .Where(cc => cc.CarId == 2 && cc.CustomerId == 1).FirstAsync();

        Assert.That(result.EngineLitre, Is.EqualTo("10L"));
    }
    
    [Test]
    public async Task AddCustomerCarService_Should_Return_True_When_Completed()
    {
        var model = new CarServiceInputModel
        {
            CustomerId = 1,
            CarId = 3,
            ServiceTypeId = 1,
            DateStarted = DateTime.UtcNow,
            Employees = new []{1, 3}
        };
        
        var result = await _customerService.AddCustomerCarService(model);

        Assert.That(result, Is.True);
    }
    
    
    [Test]
    public async Task AddCustomerCarService_Should_Return_False_When_Failed()
    {
        var result = await _customerService.AddCustomerCarService(null!);

        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task AddCustomerCarService_Should_Add_Service_In_Database()
    {
        var model = new CarServiceInputModel
        {
            CustomerId = 1,
            CarId = 3,
            ServiceTypeId = 1,
            DateStarted = DateTime.UtcNow,
            Employees = new []{1, 3}
        };
        
        await _customerService.AddCustomerCarService(model);

        var result = await _repo.AllReadonly<Service>().CountAsync();

        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public async Task AddCustomerCarService_Should_Assign_Employee()
    {
        var model = new CarServiceInputModel
        {
            CustomerId = 1,
            CarId = 3,
            ServiceTypeId = 1,
            DateStarted = DateTime.UtcNow,
            Employees = new []{1, 3}
        };
        
        await _customerService.AddCustomerCarService(model);

        var result = await _repo.AllReadonly<ServiceEmployee>()
            .Where(se => se.ServiceId == 7 && se.EmployeeId == 1).FirstOrDefaultAsync();

        Assert.That(result, Is.Not.Null);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}