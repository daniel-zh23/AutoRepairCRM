using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Employee;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AutoRepairCRM.UnitTests.Services;

[TestFixture]
public class EmployeeServiceTests
{
    private IRepository _repo = null!;
    private IEmployeeService _employeeService = null!;
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

        var user = new ApplicationUser
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
            IsFirstLogin = false
        };

        var accountService = new Mock<IAccountService>();
        accountService
            .Setup(s => s.CreateEmployee(new EmployeeInputModel()))
            .ReturnsAsync(user);

        var data = new List<string>();
        accountService
            .Setup(s => s.GetRolesForUser(It.IsAny<ApplicationUser>()))
            .ReturnsAsync((ApplicationUser u) =>
            {
                var result = _repo.AllReadonly<IdentityUserRole<string>>()
                    .Where(ur => ur.UserId == u.Id)
                    .Select(r => r.RoleId).ToList();

                return _repo.AllReadonly<IdentityRole>()
                    .Where(r => result.Contains(r.Id))
                    .Select(r => r.Name).ToList();
            });

        accountService.Setup(s => s.Deactivate(It.IsAny<string>()))
            .Callback((string userId) =>
            {
                var u = _repo.GetByIdAsync<ApplicationUser>(userId).Result;

                u.IsActive = false;
                _repo.SaveChangesAsync().Wait();
            });

        _employeeService = new EmployeeService(_repo, accountService.Object);
    }

    [Test]
    public async Task All_Should_Return_Correct_Total()
    {
        var result = await _employeeService.All();

        Assert.That(result.Total, Is.EqualTo(3));
    }

    [Test]
    public async Task All_Should_Return_Filtered_By_Search()
    {
        var result = await _employeeService.All("Worker", EmployeeSorting.Newest, EmployeeFilter.All, 1, 20);

        Assert.That(result.Items.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task All_Should_Return_Filtered_By_Role()
    {
        var result = await _employeeService.All(null!, EmployeeSorting.Newest, EmployeeFilter.Office, 1, 20);

        Assert.That(result.Items.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public async Task All_Should_Return_Sorted_By_Salary()
    {
        var result = await _employeeService.All(null!, EmployeeSorting.SalaryDesc, EmployeeFilter.All, 1, 20);

        Assert.That(result.Items.First().FirstName, Is.EqualTo("Worker2"));
    }

    [Test]
    public async Task GetServices_Should_Return_Correct_Amount()
    {
        var result = await _employeeService.GetServices(1, ServiceSorting.All, 1, 20);

        Assert.That(result.Items.Count(), Is.EqualTo(4));
    }

    [TestCase(ServiceSorting.Finished, 3)]
    [TestCase(ServiceSorting.NotFinished, 1)]
    public async Task GetServices_Should_Filter_Services(ServiceSorting sorting, int expect)
    {
        var result = await _employeeService.GetServices(1, sorting, 1, 20);

        Assert.That(result.Items.Count(), Is.EqualTo(expect));
    }

    [Test]
    public async Task GetServices_Should_Return_Total_For_Employee()
    {
        var result = await _employeeService.GetServices(1, ServiceSorting.All, 1, 20);

        Assert.That(result.Total, Is.EqualTo(4));
    }

    [TestCase("MockUser3", 1)]
    [TestCase("MockUser4", 2)]
    [TestCase("MockUser9", 3)]
    public async Task GetEmployeeId_Should_Return_Id(string userId, int expected)
    {
        var result = await _employeeService.GetEmployeeId(userId);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task AllForForm_Should_Return_Correct_Amount()
    {
        var result = await _employeeService.AllForForm();

        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task Add_Should_Return_Id()
    {
        var user = new ApplicationUser
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
            IsFirstLogin = false
        };

        var inputModel = new EmployeeInputModel
        {
            Salary = 999,
            Bonus = 10
        };

        var result = await _employeeService.Add(inputModel, user);

        Assert.That(result, Is.EqualTo(4));
    }

    [Test]
    public async Task Add_Should_Return_Negative_One_On_Fail()
    {
        var user = new ApplicationUser
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
            IsFirstLogin = false
        };

        var result = await _employeeService.Add(null!, user);

        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public async Task Add_Should_Add_Employee_In_Database()
    {
        var user = new ApplicationUser
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
            IsFirstLogin = false
        };
        var inputModel = new EmployeeInputModel
        {
            Salary = 999,
            Bonus = 10
        };

        await _employeeService.Add(inputModel, user);
        var result = await _repo.AllReadonly<Employee>().CountAsync();

        Assert.That(result, Is.EqualTo(4));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task Exists_Should_Return_True_When_Found(int id)
    {
        var result = await _employeeService.Exists(id);

        Assert.That(result, Is.True);
    }

    [TestCase(100000)]
    [TestCase(-10)]
    public async Task Exists_Should_Return_False_When_Not_Found(int id)
    {
        var result = await _employeeService.Exists(id);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetAllRoles_Should_Return_All_Roles_Except_Owner_And_Customer()
    {
        var collection = await _employeeService.GetAllRoles();
        var result = collection.Select(r => r.Name).ToList();

        CollectionAssert.DoesNotContain(result, "Owner");
        CollectionAssert.DoesNotContain(result, "Customer");
    }

    [Test]
    public async Task GetAllRoles_Should_Return_Correct_Amount()
    {
        var result = await _employeeService.GetAllRoles();

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task RolesExist_Should_Return_True_When_Found()
    {
        var result = await _employeeService.RolesExist("OfficeEmployee");

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task RolesExist_Should_Return_False_When_Not_Found()
    {
        var result = await _employeeService.RolesExist("Test");

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Fire_Should_Return_True_When_Completed()
    {
        var result = await _employeeService.Fire(1);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Fire_Should_Return_True_When_Not_Completed()
    {
        var result = await _employeeService.Fire(-10);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Fire_Should_Return_Set_IsActive_To_False()
    {
        await _employeeService.Fire(1);
        var result = await _repo.AllReadonly<Employee>()
            .Where(e => e.Id == 1).FirstAsync();

        Assert.That(result.IsActive, Is.False);
    }

    [Test]
    public async Task GetEmployeeEdit_Should_Return_Correct_Employee()
    {
        var result = await _employeeService.GetEmployeeEdit(1);

        Assert.That(result.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task Update_Should_Return_True_When_Completed()
    {
        var model = new EmployeeInputModel
        {
            FirstName = "Test",
            LastName = "WORKER",
            Email = "worker@abv.bg",
            Phone = "01234",
            Salary = 850.0m,
            Bonus = 5
        };

        var result = await _employeeService.Update(1, model);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Update_Should_Return_False_When_Failed()
    {
        var result = await _employeeService.Update(1, null!);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task Update_Should_Change_Data()
    {
        var model = new EmployeeInputModel
        {
            FirstName = "Test",
            LastName = "WORKER",
            Email = "worker@abv.bg",
            Phone = "01234",
            Salary = 850.0m,
            Bonus = 5
        };

        await _employeeService.Update(1, model);
        var result = await _repo.AllReadonly<Employee>()
            .Select(e => e.User).FirstAsync();
        
        Assert.That(result!.FirstName, Is.EqualTo("Test"));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}