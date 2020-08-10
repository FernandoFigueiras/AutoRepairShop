using AutoRepairShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoRepairShop.Web.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Brand> Brands { get; set; }


        public DbSet<Model> Models { get; set; }


        public DbSet<Vehicle> Vehicles { get; set; }


        public DbSet<Fuel> Fuels { get; set; }


        public DbSet<Color> Colors { get; set; }


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

            //Cascade Deleting Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(/*fk => fk.IsOwnership &&*/ fk => fk.DeleteBehavior == DeleteBehavior.Cascade);


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
