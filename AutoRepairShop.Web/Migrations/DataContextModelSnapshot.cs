﻿// <auto-generated />
using System;
using AutoRepairShop.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutoRepairShop.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ActiveSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Mileage");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("ScheduleDay");

                    b.Property<int?>("ServicesId");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ServicesId");

                    b.ToTable("ActiveSchedules");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandName")
                        .IsUnique();

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.BrandModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelName")
                        .IsUnique();

                    b.ToTable("Models");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int>("DistrictId");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ColorName")
                        .IsUnique();

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName")
                        .IsRequired();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CountryName")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Dealership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("City");

                    b.Property<int>("CityId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("DealerShipName")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<int>("ZipCodeId");

                    b.HasKey("Id");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("Dealerships");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.DealershipDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DealershipId");

                    b.Property<int?>("DepartmentId");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DealershipId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("DealershipDepartments");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("DepartmentName")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentName")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("DistrictName")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DealershipId");

                    b.Property<int?>("DepartmentId");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DealershipId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Fuel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("FuelType")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("FuelType")
                        .IsUnique();

                    b.ToTable("Fuels");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DepartmentId");

                    b.Property<int?>("EmployeeId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ServiceDone");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<double>("WorkHours");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.RepairHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("Dealership");

                    b.Property<int>("DealershipId");

                    b.Property<int>("EmplyeeId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LicencePlate");

                    b.Property<string>("Mileage");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("RepairDate");

                    b.Property<string>("RepairHours");

                    b.Property<string>("RepairRemarks");

                    b.Property<string>("Service");

                    b.Property<int>("ServiceId");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.ToTable("RepairHistories");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.RepairSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("RepairId");

                    b.Property<int?>("ScheduleId");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("RepairId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("RepairSchedules");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ActiveScheduleId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DealershipId");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<int?>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("ActiveScheduleId");

                    b.HasIndex("DealershipId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ScheduleDetails");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DepartmentId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ServiceDescription");

                    b.Property<string>("ServiceType")
                        .IsRequired();

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ServicesSupplied", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<int?>("DealershipId");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("ServiceId");

                    b.Property<int>("ServicesPerDay");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("DealershipId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServicesSupplied");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<bool>("CanLogin");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageUrl");

                    b.Property<bool?>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TaxPayerNumber");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("ZipCodeId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<int>("ColorId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("EngineCapacity")
                        .IsRequired();

                    b.Property<int>("FuelId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LicencePlate")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<double>("Mileage");

                    b.Property<int>("ModelId");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UserId");

                    b.Property<string>("VIN");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("FuelId");

                    b.HasIndex("LicencePlate")
                        .IsUnique();

                    b.HasIndex("ModelId");

                    b.HasIndex("UserId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ZipCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("ZipCode3")
                        .IsRequired();

                    b.Property<string>("ZipCode4")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("ZipCodes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ActiveSchedule", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Service", "Services")
                        .WithMany()
                        .HasForeignKey("ServicesId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.BrandModel", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.City", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.District", "District")
                        .WithMany("Cities")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Dealership", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.ZipCode", "ZipCode")
                        .WithMany()
                        .HasForeignKey("ZipCodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.DealershipDepartment", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Dealership", "Dealership")
                        .WithMany()
                        .HasForeignKey("DealershipId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.District", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Country", "Country")
                        .WithMany("Districts")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Employee", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Dealership", "Dealership")
                        .WithMany()
                        .HasForeignKey("DealershipId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Repair", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.RepairSchedule", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Repair", "Repair")
                        .WithMany()
                        .HasForeignKey("RepairId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.ActiveSchedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ScheduleDetail", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.ActiveSchedule", "ActiveSchedule")
                        .WithMany()
                        .HasForeignKey("ActiveScheduleId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Dealership", "Dealership")
                        .WithMany()
                        .HasForeignKey("DealershipId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Service", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ServicesSupplied", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Dealership", "Dealership")
                        .WithMany()
                        .HasForeignKey("DealershipId");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.User", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.ZipCode", "ZipCode")
                        .WithMany()
                        .HasForeignKey("ZipCodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Vehicle", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Fuel", "Fuel")
                        .WithMany()
                        .HasForeignKey("FuelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AutoRepairShop.Web.Data.Entities.BrandModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AutoRepairShop.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.ZipCode", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.City", "City")
                        .WithMany("ZipCodes")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AutoRepairShop.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
