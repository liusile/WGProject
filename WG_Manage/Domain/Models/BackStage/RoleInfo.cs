using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BackStage
{
   public class RoleInfo
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "角色代号")]
        public string RoleCode { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
    }
}
