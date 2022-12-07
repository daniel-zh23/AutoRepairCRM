using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new List<IdentityRole>()
        {
            new()
            {
                Id = "MockRole1",
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            },
            new()
            {
                Id = "MockRole3",
                Name = "Owner",
                NormalizedName = "OWNER"
            },
            new()
            {
                Id = "MockRole4",
                Name = "OfficeEmployee",
                NormalizedName = "OFFICEEMPLOYEE"
            },
            new()
            {
                Id = "MockRole5",
                Name = "Worker",
                NormalizedName = "WORKER"
            }
        });
    }
}