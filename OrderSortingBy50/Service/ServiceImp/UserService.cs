using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model;
using Domain;
using Domain.Models;
using System.Data.Entity.Migrations;

namespace Service.ServiceImp
{
    public class UserService
    {
        public void UpdateAutoLogin(Domain.Models.UserInfo userInfo,string email)
        {
            using (var db = new SortingDbContext())
            {
                var user = db.UserInfo.FirstOrDefault(o => o.UserId == userInfo.UserId);
                if (user == null)
                {
                    db.UserInfo.Add(new Domain.Models.UserInfo
                    {
                        IsAutoLogin=true,
                        LastLoginTime=DateTime.Now,
                        Pcid=userInfo.Pcid,
                        PcName=userInfo.PcName,
                        UserId=userInfo.UserId,
                        UserName=userInfo.UserName,
                        Email=email
                    });
                    db.SaveChangesAsync();
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsAutoLogin = true;
                    db.UserInfo.Add(user);
                    db.SaveChanges();
                }
            }
        }
        public void UpdateRemeberUserName(Domain.Models.UserInfo userInfo, string email)
        {
            using (var db = new SortingDbContext())
            {
                var user = db.UserInfo.FirstOrDefault(o => o.UserId == userInfo.UserId);
                if (user == null)
                {
                    db.UserInfo.Add(new Domain.Models.UserInfo
                    {
                        IsRemeberUserName = true,
                        LastLoginTime = DateTime.Now,
                        Pcid = userInfo.Pcid,
                        PcName = userInfo.PcName,
                        UserId = userInfo.UserId,
                        UserName = userInfo.UserName,
                        Email = email
                    });
                    db.SaveChangesAsync();
                }
                else
                {
                    user.LastLoginTime = DateTime.Now;
                    user.IsRemeberUserName = true;
                    db.UserInfo.AddOrUpdate(user);
                    db.SaveChanges();
                }
            }
        }
        public Domain.Models.UserInfo GetLastUserInfo()
        {
            using (var db = new SortingDbContext())
            {
                return db.UserInfo.OrderByDescending(o => o.LastLoginTime).FirstOrDefault();
            }
        }

        public void Delete(Domain.Models.UserInfo curUser)
        {
            using (var db = new SortingDbContext())
            {
                var user = db.UserInfo.FirstOrDefault(o => o.UserId == curUser.UserId);
                db.UserInfo.Remove(user);
                db.SaveChanges();
            }
        }
    }
}
