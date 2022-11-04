using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairCRM.Database.Data;

public class AutoRepairCrmDbContext : IdentityDbContext
{
    public AutoRepairCrmDbContext(DbContextOptions<AutoRepairCrmDbContext> options)
    : base(options)
    {
        
    }
}