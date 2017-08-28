using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace OrderSortingBy50.Model
{
    class SortingDetailView
    {
        internal LatticeStatus latticeStatus;

        public bool IsPrint { get; internal set; }
        public string LatticeNo { get; internal set; }
        public string OrderNo { get; internal set; }
        public string BarCode { get; internal set; }
        public string ProductName { get; internal set; }
        public ProductStatus ProductStatus { get; internal set; }
        public int PutNum { get; internal set; }
        public int WaitNum { get; internal set; }
        public string WaveNo { get; internal set; }
    }
}
