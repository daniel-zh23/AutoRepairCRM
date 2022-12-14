using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class CustomerCarConfiguration : IEntityTypeConfiguration<CustomerCar>
{
    public void Configure(EntityTypeBuilder<CustomerCar> builder)
    {
        builder.HasData(GetAll());
    }

    private List<CustomerCar> GetAll()
    {
        return new List<CustomerCar>()
        {
            new()
            {
                CustomerId = 1,
                CarId = 2,
                LicensePlate = "В 5487 СМ",
                EngineLitre = "3.5L",
                FuelTypeId = 2
            },
            new()
            {
                CustomerId = 1,
                CarId = 3,
                LicensePlate = "В 8866 ТМ",
                EngineLitre = "2.5L",
                FuelTypeId = 2
            },
            new()
            {
                CustomerId = 1,
                CarId = 1,
                LicensePlate = "В 1277 КН",
                EngineLitre = "3.0L",
                FuelTypeId = 1
            },
            new()
            {
                CustomerId = 1,
                CarId = 4,
                LicensePlate = "СВ 2366 НН",
                EngineLitre = "2.0L",
                FuelTypeId = 2
            },
        };
    }
}