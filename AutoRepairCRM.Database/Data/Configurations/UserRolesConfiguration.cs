using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(GetAll());
    }

    private List<IdentityUserRole<string>> GetAll()
    {
        return new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = "MockUser1",
                RoleId = "MockRole3"
            },
            new()
            {
                UserId = "MockUser2",
                RoleId = "MockRole1"
            },
            new()
            {
                UserId = "MockUser3",
                RoleId = "MockRole5"
            },
            new()
            {
                UserId = "MockUser4",
                RoleId = "MockRole4"
            },
            new()
            {
                UserId = "MockUser9",
                RoleId = "MockRole5"
            }
        };
    }
}