using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class MyDbContext:DbContext
    {
        public DbSet<Student> Student { get; set; }

        public MyDbContext(){
           // this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
