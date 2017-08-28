using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get;  set; }
        public string UserName { get; set; }
        public string Pcid { get;  set; }
        public string PcName { get;  set; }
        public bool IsRemeberUserName { get; set; }
        public bool IsAutoLogin { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string Email { get; set; }

        public  void CopyValues(UserInfo target)
        {
            this.Id = target.Id;
            this.UserName = target.UserName;
            this.UserId = target.UserId;
            this.Pcid = target.Pcid;
            this.PcName = target.PcName;
            this.IsRemeberUserName = target.IsRemeberUserName;
            this.IsAutoLogin = target.IsAutoLogin;
            this.LastLoginTime = target.LastLoginTime;
            this.Email = target.Email;
        }
    }
}