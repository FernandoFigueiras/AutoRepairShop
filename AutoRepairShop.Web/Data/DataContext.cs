using AutoRepairShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoRepairShop.Web.Data
{
    public class DataContext : DbContext
    {

        public DbSet<Brand> Brands { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.BrandName)
                .IsUnique();




            //Cascade Deleting Rule
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);


            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }



            base.OnModelCreating(modelBuilder);
        }

    }
}
