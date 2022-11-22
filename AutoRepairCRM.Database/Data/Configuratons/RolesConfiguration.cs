using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new List<IdentityRole>()
        {
            new()
            {
                Id = "MockRole1",
                Name = "Customer"
            },
            new()
            {
                Id = "MockRole2",
                Name = "Manager"
            },
            new()
            {
                Id = "MockRole3",
                Name = "Owner"
            },
            new()
            {
                Id = "MockRole4",
                Name = "OfficeEmployee"
            },
        });
    }
}