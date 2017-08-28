using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WaveApi
    {
        /// <summary>波次号
        /// </summary>
        [Key]
        public string WaveNo { get; set; }
        /// <summary>波次状态0：未作业 1：已作业 2:已完成
        /// </summary>
        public WaveStatus Status { get; set; } = WaveStatus.Unwork;
        /// <summary>最后更新时间
        /// </summary>
        public DateTime? LastTime { get; set; }
        public  virtual List<OrderApi> OrderApi { get; set; }
    }

    public class OrderApi
    {
        public int Id { get; set; }
        /// <summary>订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>1正常2禁用
        /// </summary>
        public OrderStatus Status { get; set; }
        public WaveApi WaveApi { get; set; }
        public string WaveApi_WaveNo { get; set; }
        public virtual List<ProductApi> ProductApi { get; set; }
    }

    public class ProductApi
    {
        public int Id { get; set; }
        /// <summary> 商品条码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string PropertyCode { get; set; }
        /// <summary>
        /// 真实的产品条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 商品特性
        /// </summary>
        public string Feature { get; set; }
        /// <summary> 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary> 拣选数量
        /// </summary>
        public int Num { get; set; }
        public OrderApi OrderApi { get; set; }
        public int OrderApi_Id { get; set; }
    }
    public enum WaveStatus
    {
        Unwork,
        Work,
        Complete
    }
    public enum OrderStatus
    {
        Normal=1,
        Drop=2
    }
}
