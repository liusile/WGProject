using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class AcceptOrder
    {
        [Key]
        public int ID { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string AcceptOrderNo { get; set; }
        public string Accepter { get; set; }
        public string AcceptDate { get; set; }
        public int AcceptNum { get; set; }
        //状态
        public string Status { get; set; }
        //备注
        public string Remark { get; set; }
        //最后操作人
        public string Oper { get; set; }
        //总金额
        public float SumPrice { get; set; }
        public virtual List<AcceptOrderDetail> AcceptOrderDetail { get; set; }

    }
}
