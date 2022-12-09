﻿// <auto-generated />
using System;
using AutoRepairCRM.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoRepairCRM.Database.Migrations
{
    [DbContext(typeof(AutoRepairCrmDbContext))]
    [Migration("20221209133847_ActiveAccountStatus")]
    partial class ActiveAccountStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Make = "BMW",
                            Model = "F82",
                            Year = "2014 - 2019"
                        },
                        new
                        {
                            Id = 2,
                            Make = "BMW",
                            Model = "G30",
                            Year = "2016 – 2023"
                        },
                        new
                        {
                            Id = 3,
                            Make = "BMW",
                            Model = "E46",
                            Year = "1997 – 2006"
                        },
                        new
                        {
                            Id = 4,
                            Make = "Mercedes",
                            Model = "W212",
                            Year = "2010 – 2016"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserId = "MockUser2"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.CustomerCar", b =>
                {
                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("EngineLitre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FuelTypeId")
                        .HasColumnType("int");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FuelTypeId");

                    b.ToTable("CustomersCars");

                    b.HasData(
                        new
                        {
                            CarId = 2,
                            CustomerId = 1,
                            EngineLitre = "3.0L",
                            FuelTypeId = 1,
                            LicensePlate = "В 5487 СМ"
                        },
                        new
                        {
                            CarId = 3,
                            CustomerId = 1,
                            EngineLitre = "2.5L",
                            FuelTypeId = 2,
                            LicensePlate = "В 8866 ТМ"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Salary = 250.0m,
                            UserId = "MockUser3"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.FuelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FuelTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Petrol"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Diesel"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Electric"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEnded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("ServiceTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceTypeId");

                    b.HasIndex("CarId", "CustomerId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CarId = 2,
                            CustomerId = 1,
                            DateEnded = new DateTime(2022, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStarted = new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsFinished = true,
                            Price = 1350.20m,
                            ServiceTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            CarId = 2,
                            CustomerId = 1,
                            DateStarted = new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsFinished = false,
                            ServiceTypeId = 2
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.ServiceEmployee", b =>
                {
                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("ServiceId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ServicesEmployees");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.ServiceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("ServiceTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Engine"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Suspension"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Drivetrain"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "MockRole1",
                            ConcurrencyStamp = "2d36ea20-8171-4792-ae04-802506b48edd",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        },
                        new
                        {
                            Id = "MockRole3",
                            ConcurrencyStamp = "cee05f54-306b-4eaf-9cba-8b849c6c65b8",
                            Name = "Owner",
                            NormalizedName = "OWNER"
                        },
                        new
                        {
                            Id = "MockRole4",
                            ConcurrencyStamp = "cc956113-d6fe-4aa4-a724-b5ffb7b76250",
                            Name = "OfficeEmployee",
                            NormalizedName = "OFFICEEMPLOYEE"
                        },
                        new
                        {
                            Id = "MockRole5",
                            ConcurrencyStamp = "71707bdf-c231-4771-b0eb-ae6566c0b41d",
                            Name = "Worker",
                            NormalizedName = "WORKER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "MockUser1",
                            RoleId = "MockRole3"
                        },
                        new
                        {
                            UserId = "MockUser2",
                            RoleId = "MockRole1"
                        },
                        new
                        {
                            UserId = "MockUser3",
                            RoleId = "MockRole5"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Account.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFirstLogin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "MockUser2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f591377f-adb8-4776-ae9a-9165b7e90f0d",
                            Email = "customer@abv.bg",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER@ABV.BG",
                            NormalizedUserName = "CUSTOMER CUSTOMER",
                            PasswordHash = "AQAAAAEAACcQAAAAELYFW1b6tSGT/Qhpiju9r+chQYAvg/znHF0s8GsFo0/S/tt6CLpCsoWIEQRpfKbLNQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "052d90f3-faf5-4e0e-b156-203bf971d7cd",
                            TwoFactorEnabled = false,
                            UserName = "customer@abv.bg",
                            FirstName = "Customer",
                            IsActive = true,
                            IsFirstLogin = true,
                            LastName = "Customer"
                        },
                        new
                        {
                            Id = "MockUser1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5b0a93b2-6e67-4d7e-a356-d034d7f185ef",
                            Email = "admin@abv.bg",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ABV.BG",
                            NormalizedUserName = "ADMIN ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAENYTXn1YxipuTIb33TvBPKeSHLiq5BV1n3qXj+KX1xmGW0e0etqMwEPAblHxHG59kQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e44c92bf-1b13-4e53-98a5-89ff9a873508",
                            TwoFactorEnabled = false,
                            UserName = "admin@abv.bg",
                            FirstName = "Admin",
                            IsActive = true,
                            IsFirstLogin = true,
                            LastName = "Admin"
                        },
                        new
                        {
                            Id = "MockUser3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9a0667d6-5c7c-4c72-9b32-bce689076d3a",
                            Email = "employee@abv.bg",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "EMPLOYEE@ABV.BG",
                            NormalizedUserName = "EMPLOYEE EMPLOYEE",
                            PasswordHash = "AQAAAAEAACcQAAAAED3aN9Utskz+iRUVPayM0FgYJv+D5d/Ahk/phyV5uJeeHYOjvsuG0Raw77Kpo4QVLg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d9c1c299-28e3-4adc-b117-586855963488",
                            TwoFactorEnabled = false,
                            UserName = "employee@abv.bg",
                            FirstName = "Employee",
                            IsActive = true,
                            IsFirstLogin = true,
                            LastName = "Employee"
                        });
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Customer", b =>
                {
                    b.HasOne("AutoRepairCRM.Database.Data.Models.Account.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.CustomerCar", b =>
                {
                    b.HasOne("AutoRepairCRM.Database.Data.Models.Car", "Car")
                        .WithMany("CustomerCars")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoRepairCRM.Database.Data.Models.Customer", "Customer")
                        .WithMany("CustomerCars")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoRepairCRM.Database.Data.Models.FuelType", "FuelType")
                        .WithMany()
                        .HasForeignKey("FuelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");

                    b.Navigation("FuelType");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Employee", b =>
                {
                    b.HasOne("AutoRepairCRM.Database.Data.Models.Account.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Service", b =>
                {
                    b.HasOne("AutoRepairCRM.Database.Data.Models.ServiceType", "ServiceType")
                        .WithMany()
                        .HasForeignKey("ServiceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoRepairCRM.Database.Data.Models.CustomerCar", "CustomerCar")
                        .WithMany("Services")
                        .HasForeignKey("CarId", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerCar");

                    b.Navigation("ServiceType");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.ServiceEmployee", b =>
                {
                    b.HasOne("AutoRepairCRM.Database.Data.Models.Employee", "Employee")
                        .WithMany("ServicesEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoRepairCRM.Database.Data.Models.Service", "Service")
                        .WithMany("ServicesEmployees")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Car", b =>
                {
                    b.Navigation("CustomerCars");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Customer", b =>
                {
                    b.Navigation("CustomerCars");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.CustomerCar", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Employee", b =>
                {
                    b.Navigation("ServicesEmployees");
                });

            modelBuilder.Entity("AutoRepairCRM.Database.Data.Models.Service", b =>
                {
                    b.Navigation("ServicesEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
