using AutoRepairCRM.Database.Data.Configuratons;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Database.Extensions;

public static class ModelBuilderExtension
{
    /// <summary>
    /// Extension method of the ModelBuilder to add mock data in the database.
    /// </summary>
    /// <param name="builder"></param>
    public static void ApplyMockData(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RolesConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new CarConfiguration());
        builder.ApplyConfiguration(new CustomerCarConfiguration());
        builder.ApplyConfiguration(new FuelTypeConfiguration());
        builder.ApplyConfiguration(new ServiceTypeConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRolesConfiguration());
    }
}