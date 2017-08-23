using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { set; get; }
        public DbSet<Module> Module { set; get; }
        public DbSet<RoleInfo> RoleInfo { set; get; }
        public DbSet<RoleModulePermission> RoleModulePermission { set; get; }
        
        public DbSet<Supplier> Supplier { set; get; }
        public DbSet<PurchaseOrder> PurchaseOrder { set; get; }
        public DbSet<SupplierProduct> SupplierProduct { set; get; }
        public DbSet<AcceptOrder> AcceptOrder { set; get; }
        public DbSet<Product> Product { set; get; }
        
        public DbSet<ProductLog> ProductLog { set; get; }
        
        public DbSet<ProductPutOutOrder> ProductPutOutOrder { set; get; }
        public DbSet<BOM> BOM { set; get; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
