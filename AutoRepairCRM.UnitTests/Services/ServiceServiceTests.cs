using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Models.Services;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.UnitTests.Services;

[TestFixture]
public class ServiceServiceTests
{
    private IRepository _repo = null!;
    private IServiceService _serviceService = null!;
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
    public async Task GetActiveServices_Should_Return_Correct_Amount()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);
        
        var result = _serviceService.GetActiveServices().Result.Count();

        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public async Task Exists_Should_Return_True_When_Found()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);
        
        var result = await _serviceService.Exists(2);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task Exists_Should_Return_False_When_Not_Found()
    {
        var repo = new Repository(_context);
        _serviceService = new ServiceService(repo);

        var result = await _serviceService.Exists(6);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task CompleteService_Should_Mark_Service_As_Finished()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);
        
        var result = await _serviceService.CompleteService(new ServiceCompleteModel() { Id = 5, Price = 10 });
        var completedCount = _repo
            .AllReadonly<Service>()
            .Count(s => s.IsFinished == true);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(completedCount, Is.EqualTo(4));
        });
    }
    
    [Test]
    public async Task ServiceTypeExists_Should_Return_True_On_Found()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);

        var result = await _serviceService.ServiceTypeExists(1);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public async Task ServiceTypeExists_Should_Return_False_On_Not_Found()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);

        var result = await _serviceService.ServiceTypeExists(-1);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public async Task GetServiceTypes_Should_Return_Correct_Amount_Of_Data()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);

        var result = await _serviceService.GetServiceTypes();
        
        Assert.That(result.Count(), Is.EqualTo(3));
    }
    
    [Test]
    public async Task GetServicesById_Should_Return_Correct_Amount_Of_Services_For_Car()
    {
        _repo = new Repository(_context);
        _serviceService = new ServiceService(_repo);

        var result = await _serviceService.GetServicesById(2, 1);
        
        Assert.That(result.Services.Count(), Is.EqualTo(4));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}