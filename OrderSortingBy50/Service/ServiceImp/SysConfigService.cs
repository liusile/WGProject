using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity.Migrations;

namespace Service.ServiceImp
{
    public class SysConfigService
    {
        public SysConfig Get()
        {
            using (var db = new SortingDbContext())
            {
                return db.SysConfig.FirstOrDefault();
            }
        }
        public bool Save(SysConfig entity)
        {
            using (var db = new SortingDbContext())
            {
                 db.SysConfig.AddOrUpdate(entity);
               return db.SaveChanges()>0;
            }
        }
    }
}
