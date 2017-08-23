using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Module
    {
        [Key]
        public int ID { get; set; }
        public int ModuleCode { get; set; }
        public int ParentModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PermissionName { get; set; }
        public int Sort { get; set; }
    }
}
