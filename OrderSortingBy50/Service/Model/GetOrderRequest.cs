using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class GetOrderRequest
    {
        public string beginUpdateTime { get; internal set; }
        public string endUpdateTime { get; internal set; }
        public string processCenterId { get; internal set; }
       // public string Token { get; internal set; }
    }
}
