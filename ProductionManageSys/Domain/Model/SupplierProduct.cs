using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class SupplierProduct
    {
        [Key]
        [Column(Order = 1)]
        public int SupplierId { get; set; }
        [Key]
        [Column(Order = 2),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Remark { get; set; }
    }
}
