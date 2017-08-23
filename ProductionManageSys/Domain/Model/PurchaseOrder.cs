using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class PurchaseOrder
    {
        [Key]
        public int ID { get; set; }
        //采购订单号
        public string PurchaseOrderNo { get; set; }
        //摘要
        public string Content { get; set; }
        //采购人
        public string Purchaser { get; set; }
        //采购日期
        public string PurchaseDate { get; set; }
        //预计到货日期
        public string PreCompleteDate { get; set; }
        //供应商
        public string Supplier { get; set; }
        //总金额
        public float SumPrice { get; set; }
        //状态
        public string Status { get; set; }
        //备注
        public string Remark { get; set; }
        //最后操作人
        public string Oper { get; set; }
        //采购总数
        public int PurchaseNum { get; set; }
        //待采购数
        public int PurchaseNumWait { get; set; }
        //已采购数
        public int PurchaseNumOver { get; set; }
        public virtual List<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
    }
}
