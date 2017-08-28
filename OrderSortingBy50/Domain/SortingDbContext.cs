  using Domain.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Domain
{
    public class SortingDbContext:DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<SlaveInfo> SlaveInfo { get; set; }
        public DbSet<WaveApi> WaveApi { get; set; }
        public DbSet<OrderApi> OrderApi { get; set; }
        public DbSet<ProductApi> ProductApi { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<OrderApi>()
                         .HasRequired(p => p.WaveApi)
                         .WithMany(user => user.OrderApi)
                         .HasForeignKey(p => p.WaveApi_WaveNo).WillCascadeOnDelete();
            modelBuilder.Entity<ProductApi>()
                        .HasRequired(p => p.OrderApi)
                        .WithMany(user => user.ProductApi)
                        .HasForeignKey(p => p.OrderApi_Id).WillCascadeOnDelete();
        }
    }
}
