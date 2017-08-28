using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class SysConfig
    {
        public int Id { get; set; }
        public string IP { get; set; }

        //界面一行显示格口数
        public int LatticeLineCount { get; set; }
        public string Port { get; set; }
        public PrintType PrintType { get; set; }
        public string ScanPortName { get; set; }
        /// <summary>域名
        /// </summary>
        public string DomainName { get; set; }
    }

    public enum PrintType
    {
        一次性打印,
        逐个打印
    }
}
