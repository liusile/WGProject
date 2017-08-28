using System.Collections.Generic;
using Domain.Models;

namespace Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domain.SortingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //重新设置默认AppDomain url
            //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string relative = @"..\..\..\OrderSortingBy50\bin\Debug\";
            //string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            //AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }

        protected override void Seed(Domain.SortingDbContext context)
        {
            //context.User.AddOrUpdate(new User()
            //{
            //    Name = "liusile"
            //});
            //context.SaveChanges();
        }
    }
}
