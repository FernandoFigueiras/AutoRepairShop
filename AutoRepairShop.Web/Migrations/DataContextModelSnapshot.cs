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

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandName")
                        .IsUnique();

                    b.ToTable("Brands");
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

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Model", b =>
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
                        .HasMaxLength(20);

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelName")
                        .IsUnique();

                    b.ToTable("Models");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId");

                    b.Property<string>("ColorId");

                    b.Property<int?>("ColorId1");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<DateTime?>("DeactivationDate");

                    b.Property<string>("EngineCapacity")
                        .IsRequired();

                    b.Property<int>("FuelId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LicencePlate")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("ModelId");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ColorId1");

                    b.HasIndex("FuelId");

                    b.HasIndex("LicencePlate")
                        .IsUnique();

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Model", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AutoRepairShop.Web.Data.Entities.Vehicle", b =>
                {
                    b.HasOne("AutoRepairShop.Web.Data.Entities.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId1");

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Fuel", "Fuel")
                        .WithMany()
                        .HasForeignKey("FuelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AutoRepairShop.Web.Data.Entities.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
