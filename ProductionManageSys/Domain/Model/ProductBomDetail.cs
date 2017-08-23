using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public  class ProductBomDetail
    {
        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductName { get; set; }
    }
}
