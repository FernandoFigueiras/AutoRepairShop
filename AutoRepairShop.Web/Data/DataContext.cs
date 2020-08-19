using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace AutoRepairShop.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Brand> Brands { get; set; }


        public DbSet<Model> Models { get; set; }


        public DbSet<Vehicle> Vehicles { get; set; }


        public DbSet<Fuel> Fuels { get; set; }


        public DbSet<Color> Colors { get; set; }


        public DbSet<Country> Countries { get; set; }


        public DbSet<District> Districts { get; set; }


        public DbSet<County> Counties { get; set; }


        public DbSet<ZipCode> ZipCodes { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.BrandName)
                .IsUnique();


            modelBuilder.Entity<Model>()
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


            //modelBuilder.Entity<District>()
            // .HasIndex(d => d.DistrictName)
            // .IsUnique();


            modelBuilder.Entity<County>()
              .HasIndex(c => c.CountyName)
              .IsUnique();


           

            //Cascade Deleting Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);


            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
                //if (fk.DependentToPrincipal.DeclaringEntityType.GetType().FullName == "Vehicle")
                //{
                //    fk.DeleteBehavior = DeleteBehavior.Cascade;
                //}
               
            }



            base.OnModelCreating(modelBuilder);
        }

    }
}
