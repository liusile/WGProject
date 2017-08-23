using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class BOM
    {
        [Key]
        public int ID { get; set; }

        public string ModuleName { get; set; }

        public string Type { get; set; }
        public string BOMStatus { get; set; }
        public string CreatePeople { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastOper { get; set; }
        public DateTime LastOperTime { get; set; }

        public int ParentModuleID { get; set; }
        public int ModuleNum { get; set; }

        public string TopModuleName { get; set; }

        public virtual List<ProductBOM> ProductBom { get; set; }
        

    }
}
