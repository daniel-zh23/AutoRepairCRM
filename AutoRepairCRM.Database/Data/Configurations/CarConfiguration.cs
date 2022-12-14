using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasData(GetCars());
    }

    public IList<Car> GetCars()
    {
        return new List<Car>()
        {
            new()
            {
                Id = 1,
                Make = "BMW",
                Model = "F82",
                Year = "2014 - 2019",
                IsActive = true
            },
            new()
            {
                Id = 2,
                Make = "BMW",
                Model = "G30",
                Year = "2016 – 2023",
                IsActive = true
            },
            new()
            {
                Id = 3,
                Make = "BMW",
                Model = "E46",
                Year = "1997 – 2006",
                IsActive = true
            },
            new()
            {
                Id = 4,
                Make = "Mercedes",
                Model = "W212",
                Year = "2010 – 2016",
                IsActive = true
            },
            new()
            {
                Id = 5,
                Make = "Mercedes",
                Model = "W124",
                Year = "1984 - 1997",
                IsActive = true
            },
            new()
            {
                Id = 6,
                Make = "Mercedes",
                Model = "W212",
                Year = "2010 – 2016",
                IsActive = true
            },
        };
    }
}