using AutoRepairCRM.Core.Contracts;
using AutoRepairCRM.Core.Services;
using AutoRepairCRM.Database.Data;
using AutoRepairCRM.Database.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.UnitTests.Services;

[TestFixture]
public class RevenueServiceTests
{
    private IRepository _repo = null!;
    private IRevenueService _revenueService = null!;
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
        _revenueService = new RevenueService(_repo);
    }

    [Test]
    public async Task GetRevenue_Should_Return_Correct_Total_Revenue()
    {
        var result = await _revenueService.GetRevenue(new DateTime(2022, 12, 1));
        
        Assert.That(result.RevenueThisMonth, Is.EqualTo(3500.33m));
    }
    
    [Test]
    public async Task GetRevenue_Should_Return_Correct_Profit()
    {
        var result = await _revenueService.GetRevenue(new DateTime(2022, 12, 1));
        
        Assert.That(result.Profit, Is.EqualTo(-224.6865m));
    }
    
    [Test]
    public async Task GetRevenue_Should_Return_Correct_Amount_Of_Employees()
    {
        var result = await _revenueService.GetRevenue(new DateTime(2022, 12, 1));
        
        Assert.That(result.Employees.Count(), Is.EqualTo(3));
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }
}