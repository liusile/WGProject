using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SubBOM
    {
        [Key]
        public int ID { get; set; }
        public string BOMName { get; set; }
        public int Num { get; set; }
    }
}
