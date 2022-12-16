using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(GetAll());
    }

    private IEnumerable<Customer> GetAll()
    {
        return new List<Customer>
        {
            new()
            {
                Id = 1,
                UserId = "MockUser2",
            },
            new()
            {
                Id = 2,
                UserId = "MockUser5",
            },
            new()
            {
                Id = 3,
                UserId = "MockUser6",
            },
            new()
            {
                Id = 4,
                UserId = "MockUser7",
            },
            new()
            {
                Id = 5,
                UserId = "MockUser8",
            },
        };
    }
}