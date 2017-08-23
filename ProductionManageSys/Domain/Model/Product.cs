using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int SumNum { get; set; }
        public int LockNum { get; set; }
        public string From { get; set; }
        public string Remark { get; set; }
        public int? WaringMin { get; set; }
        public int? WaringMax { get; set; }
        public string Location { get; set; }
        public string  Type { get; set; }

    }

}
