using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ProductBOM
    {
        [Key]
        [Column(Order = 1)]
        public int BOMID { get; set; }
        [Key]
        [Column(Order = 3)]
        public string ProductName { get; set; }
        public int Num { get; set; }

    }
}
