using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System.Linq;

namespace AutoRepairShop.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Brand> Brands { get; set; }



        public DbSet<BrandModel> Models { get; set; }



        public DbSet<Vehicle> Vehicles { get; set; }



        public DbSet<Fuel> Fuels { get; set; }



        public DbSet<Color> Colors { get; set; }



        public DbSet<Country> Countries { get; set; }



        public DbSet<District> Districts { get; set; }



        public DbSet<City> Cities { get; set; }



        public DbSet<ZipCode> ZipCodes { get; set; }



        public DbSet<ActiveSchedule> ActiveSchedules { get; set; }



        public DbSet<ScheduleDetail> ScheduleDetails { get; set; }



        public DbSet<Dealership> Dealerships { get; set; }



        public DbSet<Service> Services { get; set; }



        public DbSet<DealershipService> DealershipServices { get; set; }



        public DbSet<Department> Departments { get; set; }



        public DbSet<DealershipDepartment> DealershipDepartments { get; set; }



        public DbSet<Employee> Employees { get; set; }



        public DbSet<RepairSchedule> RepairSchedules { get; set; }



        public DbSet<Repair> Repairs { get; set; }



        public DbSet<RepairHistory> RepairHistories { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            var cascadeFKs = modelBuilder.Model
             .GetEntityTypes()
             .SelectMany(t => t.GetForeignKeys())
             .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);


            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            }

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.BrandName)
                .IsUnique();


            modelBuilder.Entity<BrandModel>()
                .HasIndex(m => m.ModelName)
                .IsUnique();


            modelBuilder.Entity<Fuel>()
                .HasIndex(f => f.FuelType)
                .IsUnique();


            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.LicencePlate)
                .IsUnique();


            modelBuilder.Entity<Color>()
               .HasIndex(c => c.ColorName)
               .IsUnique();

            modelBuilder.Entity<Country>()
              .HasIndex(c => c.CountryName)
              .IsUnique();


            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentName)
                .IsUnique();






            base.OnModelCreating(modelBuilder);
        }

    }
}
