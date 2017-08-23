using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductionManage.Models
{
    public static class OrderStatus
    {
        //为什么采用中文，因为不想进行关联
        public static string 未审批 = "未审批";
        public static string 已审批 = "已审批";
        public static string 待验收 = "待验收";
        public static string 部分验收 = "部分验收";
        public static string 已验收 = "已验收";
    }
}