using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ProductPutOutOrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int ProductPutOutOrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        //产品名称
        public string ProductName { get; set; }
        //采购数量
        public int Num { get; set; }
        //产品单价
        public float Price { get; set; }
        //总金额
        public float SumPrice { get; set; }
        //备注
        public string Remark { get; set; }
    }
}
