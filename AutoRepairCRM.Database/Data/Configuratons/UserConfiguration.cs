using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

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
            UserName = "Admin",
            FirstName = "Admin",
            LastName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@abv.bg",
            NormalizedEmail = "ADMIN@ABV.BG",
            EmailConfirmed = true
        };
        var customer = new ApplicationUser()
        {
            Id = "MockUser2",
            UserName = "Customer",
            FirstName = "Customer",
            LastName = "Customer",
            NormalizedUserName = "CUSTOMER",
            Email = "customer@abv.bg",
            NormalizedEmail = "CUSTOMER@ABV.BG",
            EmailConfirmed = true
        };
        
        customer.PasswordHash = hasher.HashPassword(customer, "customer1234");
        admin.PasswordHash = hasher.HashPassword(admin, "admin1234");

        return new List<ApplicationUser>() { customer, admin };
    }
}