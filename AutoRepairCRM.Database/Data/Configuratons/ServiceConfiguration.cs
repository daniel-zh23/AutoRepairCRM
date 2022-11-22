using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasData(GetAll());
    }

    public List<Service> GetAll()
    {
        return new List<Service>()
        {
            new()
            {
                Id = 1,
                ServiceTypeId = 1,
                CarId = 2,
                CustomerId = 1,
                DateStarted = new DateTime(2022, 10, 2),
                IsFinished = true,
                DateEnded = new DateTime(2022, 10, 16),
                Price = 1350.20m
            },
            new()
            {
                Id = 2,
                ServiceTypeId = 2,
                CarId = 2,
                CustomerId = 1,
                DateStarted = new DateTime(2021, 10, 24),
                IsFinished = false
            },
        };
    }
}