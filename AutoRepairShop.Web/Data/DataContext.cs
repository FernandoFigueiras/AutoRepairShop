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
                .HasIndex(v => v.LicencePLate)
                .IsUnique();


            //Cascade Deleting Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);


            foreach (var fk in cascadeFKs)
            {
                if (fk.DependentToPrincipal.DeclaringEntityType.GetType().FullName=="Vehicle")
                {
                    fk.DeleteBehavior = DeleteBehavior.Cascade;
                }
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }



            base.OnModelCreating(modelBuilder);
        }

    }
}
