using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class RoleModulePermission
    {
        [Key]
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int ModuleID { get; set; }

    }
}
