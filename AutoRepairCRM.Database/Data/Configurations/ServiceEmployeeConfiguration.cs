using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class ServiceEmployeeConfiguration : IEntityTypeConfiguration<ServiceEmployee>
{
    public void Configure(EntityTypeBuilder<ServiceEmployee> builder)
    {
        builder.HasData(GetAll());
    }

    private IEnumerable<ServiceEmployee> GetAll()
    {
        return new List<ServiceEmployee>
        {
            new()
            {
                EmployeeId = 1,
                ServiceId = 1
            },
            new()
            {
                EmployeeId = 1,
                ServiceId = 2
            },
            new()
            {
                EmployeeId = 1,
                ServiceId = 3
            },
            new()
            {
                EmployeeId = 1,
                ServiceId = 4
            },
        };
    }
}