using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class CustomerConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser()
        {
            UserName = "Customer",
            FirstName = "Customer",
            LastName = "Customer",
            NormalizedUserName = "CUSTOMER",
            Email = "customer@abv.bg",
            NormalizedEmail = "CUSTOMER@ABV.BG",
            EmailConfirmed = true
        };
        user.PasswordHash = hasher.HashPassword(user, "customer1234");
        builder.HasData(user);
    }
}