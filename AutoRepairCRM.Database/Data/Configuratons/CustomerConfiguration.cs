using AutoRepairCRM.Database.Data.Models;
using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configuratons;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(new Customer()
        {
            Id = 1,
            UserId = "MockUser2",
        });
    }
}