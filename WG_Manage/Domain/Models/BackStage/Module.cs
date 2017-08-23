using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BackStage
{
   public class Module
    {
        [Key]
        public int ID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
   
}
