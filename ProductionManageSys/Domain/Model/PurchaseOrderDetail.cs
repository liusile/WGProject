using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class PurchaseOrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int PurchaseOrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ProductId { get; set; }
        //产品名称
        public string ProductName { get; set; }
        //采购数量
        public int PurchaseNum { get; set; }
        //待采购数
        public int PurchaseNumWait { get; set; }
        //已采购数
        public int PurchaseNumOver { get; set; }
        //产品单价
        public float Price { get; set; }
        //总金额
        public float SumPrice { get; set; }
        //备注
        public string Remark { get; set; }
    }
}