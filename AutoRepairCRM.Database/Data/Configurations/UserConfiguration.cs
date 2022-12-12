﻿using AutoRepairCRM.Database.Data.Models.Account;
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
        var customer = new ApplicationUser()
        {
            Id = "MockUser2",
            UserName = "customer@abv.bg",
            FirstName = "Customer",
            LastName = "Customer",
            NormalizedUserName = "CUSTOMER@ABV.BG",
            Email = "customer@abv.bg",
            NormalizedEmail = "CUSTOMER@ABV.BG",
            PhoneNumber = "01234",
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

        worker.PasswordHash = hasher.HashPassword(worker, "worker1234");
        customer.PasswordHash = hasher.HashPassword(customer, "customer1234");
        owner.PasswordHash = hasher.HashPassword(owner, "owner1234");
        office.PasswordHash = hasher.HashPassword(office, "office1234");

        return new List<ApplicationUser>() { customer, owner, worker, office };
    }
}