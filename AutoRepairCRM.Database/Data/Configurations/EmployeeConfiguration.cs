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
        return new List<Employee>
        {
            new()
            {
                Id = 1,
                UserId = "MockUser3",
                Salary = 850.0m,
                BonusPercent = 5
            },
            new()
            {
                Id = 2,
                UserId = "MockUser4",
                Salary = 1100.0m,
                BonusPercent = 0
            },
            new()
            {
                Id = 3,
                UserId = "MockUser9",
                Salary = 1600.0m,
                BonusPercent = 3
            }
        };
    }
}