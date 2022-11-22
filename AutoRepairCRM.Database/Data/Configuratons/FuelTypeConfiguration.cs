using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.HasData(GetAll());
    }

    public List<FuelType> GetAll()
    {
        return new List<FuelType>()
        {
            new()
            {
                Id = 1,
                Name = "Petrol"
            },
            new()
            {
                Id = 2,
                Name = "Diesel"
            },
            new()
            {
                Id = 3,
                Name = "Electric"
            }
        };
    }
}