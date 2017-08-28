using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Service.Model
{
    public class SortingDetailView
    {
        internal LatticeStatus latticeStatus;

        public string BarCode { get; internal set; }
        public bool IsPrint { get; internal set; }
        public string LatticeNo { get; internal set; }
        public string OrderNo { get; internal set; }
        public string ProductCode { get; internal set; }
        public string ProductName { get; internal set; }
        public ProductStatus ProductStatus { get; internal set; }
        public string PropertyCode { get; internal set; }
        public int PutNum { get; internal set; }
        public int WaitNum { get; internal set; }
        public string WaveNo { get; internal set; }
    }
}
