using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class CompleteWaveRequest
    {
        public string operatingTime { get; internal set; }
        public int operatorID { get; internal set; }
        public string waveNo { get; internal set; }
        public List<ShortageApi> list { get; internal set; }
    }
    public class ShortageApi
    {
        /// <summary>订单号
        /// </summary>
        public string orderNO { get; set; }
        /// <summary>商品条码
        /// </summary>
        public string productcode { get; set; }
        /// <summary> 商品条码
        /// </summary>
        public string propertyCode { get; set; }
        /// <summary>总数量
        /// </summary>
        public int num { get; set; }
        /// <summary>已有的分拣数
        /// </summary>
        public int pickingQuantity { get; set; }

    }
}
