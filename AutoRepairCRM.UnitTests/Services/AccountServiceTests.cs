using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Customer;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace AutoRepairCRM.UnitTests.Services;

[TestFixture]
public class AccountServiceTests
{
    private IRepository _repo = null!;
    private IAccountService _accountService = null!;
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
        
        var store = new Mock<IUserStore<ApplicationUser>>();
        var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        mgr.Setup(s =>
                s.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        mgr.Setup(s =>
                s.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        _accountService = new AccountService(mgr.Object, _repo);
    }

    [Test]
    public async Task CreateCustomer_Should_Return_Mapped_User()
    {
        var model = new CustomerInputModel
        {
            FirstName = "Test",
            LastName = "TEST",
            Email = "test@abv.bg",
            Phone = "1234"
        };

        var result = await _accountService.CreateCustomer(model);
        
        Assert.That(result!.FirstName, Is.EqualTo(model.FirstName));
    }
    
    [Test]
    public async Task CreateCustomer_Should_Return_Null_When_Not_Succeeded()
    {
        var result = await _accountService.CreateCustomer(null!);
        
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task CreateEmployee_Should_Return_Mapped_User()
    {
        var model = new EmployeeInputModel
        {
            FirstName = "Test",
            LastName = "TEST",
            Email = "test@abv.bg",
            Phone = "1234",
            Salary = 820m,
            Bonus = 3,
            Roles = "Worker"
        };

        var result = await _accountService.CreateEmployee(model);
        
        Assert.That(result!.FirstName, Is.EqualTo(model.FirstName));
    }
    
    [Test]
    public async Task CreateEmployee_Should_Return_Null_When_Not_Succeeded()
    {
        var result = await _accountService.CreateEmployee(null!);
        
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task ChangeFirstLoginState_Should_Return_True_When_Completed()
    {
        var model = new ApplicationUser
        {
            Id = "Mock",
            UserName = "mock@abv.bg",
            FirstName = "Mock",
            LastName = "MOCK",
            NormalizedUserName = "MOCK@ABV.BG",
            Email = "mock@abv.bg",
            NormalizedEmail = "MOCK@ABV.BG",
            PhoneNumber = "9999",
            EmailConfirmed = true,
            IsFirstLogin = true
        };
        
        var result = await _accountService.ChangeFirstLoginState(model, false);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task ChangeFirstLoginState_Should_Change_Value()
    {
        var model = new ApplicationUser()
        {
            Id = "MockUser1",
            UserName = "owner@abv.bg",
            FirstName = "Owner",
            LastName = "OWNER",
            NormalizedUserName = "OWNER@ABV.BG",
            Email = "owner@abv.bg",
            NormalizedEmail = "OWNER@ABV.BG",
            PhoneNumber = "01234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        
        await _accountService.ChangeFirstLoginState(model, true);

        Assert.That(model.IsFirstLogin, Is.True);
    }
    
    [Test]
    public async Task ChangeFirstLoginState_Should_Return_False_When_Not_Completed()
    {
        var result = await _accountService.ChangeFirstLoginState(null!, false);
        
        Assert.That(result, Is.False);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}