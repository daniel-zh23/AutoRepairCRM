using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.HasData(GetAll());
    }

    private List<ApplicationUser> GetAll()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var admin = new ApplicationUser()
        {
            Id = "MockUser1",
            UserName = "Admin ADMIN",
            FirstName = "Admin",
            LastName = "Admin",
            NormalizedUserName = "ADMIN ADMIN",
            Email = "admin@abv.bg",
            NormalizedEmail = "ADMIN@ABV.BG",
            EmailConfirmed = true
        };
        var customer = new ApplicationUser()
        {
            Id = "MockUser2",
            UserName = "Customer CUSTOMER",
            FirstName = "Customer",
            LastName = "Customer",
            NormalizedUserName = "CUSTOMER CUSTOMER",
            Email = "customer@abv.bg",
            NormalizedEmail = "CUSTOMER@ABV.BG",
            EmailConfirmed = true
        };
        var employee = new ApplicationUser()
        {
            Id = "MockUser3",
            UserName = "Employee EMPLOYEE",
            FirstName = "Employee",
            LastName = "Employee",
            NormalizedUserName = "EMPLOYEE EMPLOYEE",
            Email = "employee@abv.bg",
            NormalizedEmail = "EMPLOYEE@ABV.BG",
            EmailConfirmed = true
        };

        employee.PasswordHash = hasher.HashPassword(employee, "employee1234");
        customer.PasswordHash = hasher.HashPassword(customer, "customer1234");
        admin.PasswordHash = hasher.HashPassword(admin, "admin1234");

        return new List<ApplicationUser>() { customer, admin, employee };
    }
}