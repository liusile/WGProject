using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class AcceptOrderDetailView:AcceptOrderDetail
    {
        public int PurchaseNum { get; set; }
        public int PurchaseNumWait { get; set; }
    }
}
