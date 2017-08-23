using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BackStage
{
    public class Permission
    {
        [Key]
        public int ID { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
    }
   
}
