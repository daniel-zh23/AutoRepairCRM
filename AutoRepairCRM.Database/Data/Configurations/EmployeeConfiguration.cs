using AutoRepairCRM.Database.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(GetAll());
    }

    private IEnumerable<Employee> GetAll()
    {
        return new List<Employee>()
        {
            new()
            {
                Id = 1,
                UserId = "MockUser3",
                Salary = 250.0m
            }
        };
    }
}