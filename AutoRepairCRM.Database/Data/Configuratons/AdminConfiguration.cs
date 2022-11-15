using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class AdminConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser()
        {
            Id = "dea12856-c198-4129-b3f3-b893d8395082",
            UserName = "admin@abv.bg",
            FirstName = "Admin",
            LastName = "Admin",
            NormalizedUserName = "ADMIN@ABV.BG",
            Email = "admin@abv.bg",
            NormalizedEmail = "ADMIN@ABV.BG",
            EmailConfirmed = true
        };
        user.PasswordHash = hasher.HashPassword(user, "admin1234");
        builder.HasData(user);
    }
}