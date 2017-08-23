using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Supplier
    {
        [Key]
        public int ID { get; set; }
        public string CorpName { get; set; }
        public string Address { get; set; }
        public string People { get; set; }
        public  string Phone { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public string Remark { get; set; }
    }
}
