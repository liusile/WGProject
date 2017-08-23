using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ProductPutOutOrder
    {

        [Key]
        public int ID { get; set; }
        //订单号
        public string ProductOrderNo { get; set; }
        //摘要
        public string Content { get; set; }
        //日期
        public DateTime ProductPutOrderDate { get; set; }
        //总金额
        public float SumPrice { get; set; }
        //状态
        public string Status { get; set; }
        //备注
        public string Remark { get; set; }
        //操作人
        public string Oper { get; set; }
        //总数
        public int SumNum { get; set; }
        //入库、出库
        public string Type { get; set; }
        public virtual List<ProductPutOutOrderDetail> ProductOrderDetail { get; set; }

    }
}
