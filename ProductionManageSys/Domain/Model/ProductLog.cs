using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
     public class ProductLog
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string In1Out { get; set; }
        public int num { get; set; }
        public string Remark { get; set; }
        public DateTime OperTime { get; set; }
        public string Oper { get; set; }
    }
}
