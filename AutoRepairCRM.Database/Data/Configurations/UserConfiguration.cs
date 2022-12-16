using AutoRepairCRM.Database.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoRepairCRM.Database.Data.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.HasData(GetAll());
    }

    private List<ApplicationUser> GetAll()
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        var owner = new ApplicationUser()
        {
            Id = "MockUser1",
            UserName = "owner@abv.bg",
            FirstName = "Owner",
            LastName = "OWNER",
            NormalizedUserName = "OWNER@ABV.BG",
            Email = "owner@abv.bg",
            NormalizedEmail = "OWNER@ABV.BG",
            PhoneNumber = "01234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var customer1 = new ApplicationUser()
        {
            Id = "MockUser2",
            UserName = "customer@abv.bg",
            FirstName = "Customer",
            LastName = "Customer",
            NormalizedUserName = "CUSTOMER@ABV.BG",
            Email = "customer@abv.bg",
            NormalizedEmail = "CUSTOMER@ABV.BG",
            PhoneNumber = "11234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var worker = new ApplicationUser()
        {
            Id = "MockUser3",
            UserName = "worker@abv.bg",
            FirstName = "Worker",
            LastName = "WORKER",
            NormalizedUserName = "WORKER@ABV.BG",
            Email = "worker@abv.bg",
            NormalizedEmail = "WORKER@ABV.BG",
            PhoneNumber = "01234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var office = new ApplicationUser()
        {
            Id = "MockUser4",
            UserName = "office@abv.bg",
            FirstName = "Office",
            LastName = "OFFICE",
            NormalizedUserName = "OFFICE@ABV.BG",
            Email = "office@abv.bg",
            NormalizedEmail = "OFFICE@ABV.BG",
            PhoneNumber = "01234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var customer2 = new ApplicationUser()
        {
            Id = "MockUser5",
            UserName = "customer2@abv.bg",
            FirstName = "Ivan",
            LastName = "Customer2",
            NormalizedUserName = "CUSTOMER2@ABV.BG",
            Email = "customer2@abv.bg",
            NormalizedEmail = "CUSTOMER2@ABV.BG",
            PhoneNumber = "21234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var customer3 = new ApplicationUser()
        {
            Id = "MockUser6",
            UserName = "customer3@abv.bg",
            FirstName = "Ivan",
            LastName = "Customer3",
            NormalizedUserName = "CUSTOMER3@ABV.BG",
            Email = "customer3@abv.bg",
            NormalizedEmail = "CUSTOMER3@ABV.BG",
            PhoneNumber = "31234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var customer4 = new ApplicationUser()
        {
            Id = "MockUser7",
            UserName = "customer4@abv.bg",
            FirstName = "Georgi",
            LastName = "Customer4",
            NormalizedUserName = "CUSTOMER4@ABV.BG",
            Email = "customer4@abv.bg",
            NormalizedEmail = "CUSTOMER4@ABV.BG",
            PhoneNumber = "41234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var customer5 = new ApplicationUser()
        {
            Id = "MockUser8",
            UserName = "customer5@abv.bg",
            FirstName = "Daniel",
            LastName = "Customer5",
            NormalizedUserName = "CUSTOMER5@ABV.BG",
            Email = "customer5@abv.bg",
            NormalizedEmail = "CUSTOMER5@ABV.BG",
            PhoneNumber = "51234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };
        var worker2 = new ApplicationUser()
        {
            Id = "MockUser9",
            UserName = "worker2@abv.bg",
            FirstName = "Worker2",
            LastName = "WORKER2",
            NormalizedUserName = "WORKER2@ABV.BG",
            Email = "worker2@abv.bg",
            NormalizedEmail = "WORKER2@ABV.BG",
            PhoneNumber = "11234",
            EmailConfirmed = true,
            IsFirstLogin = false
        };

        worker.PasswordHash = hasher.HashPassword(worker, "worker1234");
        worker2.PasswordHash = hasher.HashPassword(worker2, "worker1234");
        customer1.PasswordHash = hasher.HashPassword(customer1, "customer1234");
        customer2.PasswordHash = hasher.HashPassword(customer2, "customer1234");
        customer3.PasswordHash = hasher.HashPassword(customer3, "customer1234");
        customer4.PasswordHash = hasher.HashPassword(customer4, "customer1234");
        customer5.PasswordHash = hasher.HashPassword(customer5, "customer1234");
        owner.PasswordHash = hasher.HashPassword(owner, "owner1234");
        office.PasswordHash = hasher.HashPassword(office, "office1234");

        return new List<ApplicationUser>() { customer1, customer2, customer3, customer4, customer5, owner, worker, office, worker2 };
    }
}