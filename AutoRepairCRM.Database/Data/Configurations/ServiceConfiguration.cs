using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

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
                DateStarted = new DateTime(2022, 11, 2),
                IsFinished = true,
                DateEnded = new DateTime(2022, 11, 16),
                Price = 1800.00m
            },
            new()
            {
                Id = 2,
                ServiceTypeId = 2,
                CarId = 2,
                CustomerId = 1,
                DateStarted = new DateTime(2021, 11, 24),
                IsFinished = false
            },
            new()
            {
                Id = 3,
                ServiceTypeId = 1,
                CarId = 2,
                CustomerId = 1,
                DateStarted = new DateTime(2022, 12, 2),
                IsFinished = true,
                DateEnded = new DateTime(2022, 12, 16),
                Price = 2500.23m
            },
            new()
            {
                Id = 4,
                ServiceTypeId = 3,
                CarId = 3,
                CustomerId = 1,
                DateStarted = new DateTime(2022, 12, 2),
                IsFinished = true,
                DateEnded = new DateTime(2022, 12, 16),
                Price = 1000.10m
            },
            new()
            {
                Id = 5,
                ServiceTypeId = 2,
                CarId = 2,
                CustomerId = 1,
                DateStarted = new DateTime(2022, 12, 2),
                IsFinished = false
            },
        };
    }
}