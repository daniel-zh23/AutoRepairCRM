using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.HasData(GetAll());
    }

    private List<ServiceType> GetAll()
    {
        return new List<ServiceType>()
        {
            new()
            {
                Id = 1,
                Name = "Engine"
            },
            new()
            {
                Id = 2,
                Name = "Suspension"
            },
            new()
            {
                Id = 3,
                Name = "Drivetrain"
            },
        };
    }
}