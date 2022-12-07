using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using AutoRepairCRM.Database.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Database.Data;

public class AutoRepairCrmDbContext : IdentityDbContext
{
    public AutoRepairCrmDbContext(DbContextOptions<AutoRepairCrmDbContext> options)
    : base(options)
    {
        
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerCar> CustomersCars { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceEmployee> ServicesEmployees { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<FuelType> FuelTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyMockData();
        
        
        builder.Entity<CustomerCar>()
            .HasKey(e => new { e.CarId, e.CustomerId });
        builder.Entity<ServiceEmployee>()
            .HasKey(e => new { e.ServiceId, e.EmployeeId });
        
        base.OnModelCreating(builder);
    }
}