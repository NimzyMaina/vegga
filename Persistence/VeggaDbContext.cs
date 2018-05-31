using Microsoft.EntityFrameworkCore;
using vegga.Models;

namespace vegga.Persistence
{
    public class VeggaDbContext : DbContext 
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        public VeggaDbContext(DbContextOptions<VeggaDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>()
                .HasKey(vf => new { vf.FeatureId,vf.VehicleId });
        }
        
    }
}