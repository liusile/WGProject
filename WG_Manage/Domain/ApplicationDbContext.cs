using Domain.Models.BackStage;
using Domain.Models.HeadStage;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Domain
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<UserInfo> UserInfo { set; get; }
        public DbSet<ApplyJob> Job { set; get; }
        public DbSet<RecruitJob> RecruitJob { set; get; }
        public DbSet<Company> Company { set; get; }
        public DbSet<Employee> Employee { set; get; }
        public DbSet<News> News { set; get; }
        public DbSet<Product> Product { set; get; }
        public DbSet<ProductAttribute> ProductAttribute { set; get; }
        public DbSet<Module> Module { set; get; }
        public DbSet<RoleInfo> RoleInfo { set; get; }
        public DbSet<Permission> Permission { set; get; }
        public DbSet<RoleModulePermission> RoleModulePermission { set; get; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
   
}
