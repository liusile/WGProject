using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    //感觉有点乱，可以使用部分类分开，一部分是生成数据库模型，一部分是模型操作类
    public  class SlaveInfo
    {
        public int Id { get; set; }
        public string WaveNo { get; set; }
        //总共需要分拣数量
        public int NeedWaitPutByApi { get; set; }
        public int PutNum => LatticeInfo.Sum(o => o.PutNum);
        public bool IsPrintOver => LatticeInfo.Where(o=>!string.IsNullOrWhiteSpace(o.OrderNo)).All(o=>o.IsPrint);
        public bool IsCompleteHand { get; set; } = false;//就否手动结束
        public bool IsComplete=> IsCompleteHand || 
                                (LatticeInfo.TrueForAll(o => o.IsComplete) && NeedWaitPutByApi == LatticeInfo.Sum(o=>o.PutNum)+LatticeInfo.Where(o=>o.IsCompleteHand).Sum(o=>o.WaitNum-o.PutNum));//是否完成
        public LatticeStatus LatticeStatus
        {
            get
            {
                if(LatticeInfo.Any(o => o.Status == LatticeStatus.BlockError))
                {
                    return LatticeStatus.BlockError;
                }
                else if(LatticeInfo.Any(o => o.Status == LatticeStatus.PutError))
                {
                    return LatticeStatus.PutError;
                }
                else if (LatticeInfo.Any(o => o.Status == LatticeStatus.WaitPut))
                {
                    return LatticeStatus.WaitPut;
                }
                else 
                {
                    return LatticeStatus.None;
                }
            }       
        }
        public  List<LatticeInfo> LatticeInfo { get; set; }
    }

    public class LatticeInfo
    {
        /// <summary>格口号
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string LatticeNo { get; set; }
        public string OrderNo { get; set; }
        public  List<Product> Product { get; set; }
        public bool IsPrint { get; set; } = false;
        public bool IsCompleteHand => IsPrint && WaitNum > PutNum;
        public LatticeStatus Status { get; set; }= LatticeStatus.WaitPut;
        /// <summary>
        /// 格口号是否完成
        /// </summary>
        public bool IsComplete=> IsPrint || (Product?.TrueForAll(o => o.IsComplete) ?? true);
        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor()
        {
            if (Status==LatticeStatus.WaitPut)
            {
                return Color.Green;
            }
            if (Status == LatticeStatus.BlockError || Status == LatticeStatus.PutError)
            {
                return Color.Red;
            }
            return Color.Gainsboro;
        } 
        public int WaitNum=> Product?.Sum(o => o.WaitNum) ?? 0;
        public int PutNum => Product?.Sum(o => o.PutNum) ?? 0;
        public string DisPlay()
        { 
            string text = "";
            text += $"{LatticeNo}{Environment.NewLine}{Environment.NewLine}";
            text += $"单号：{Util.ClipString(OrderNo,20) }{Environment.NewLine}{Environment.NewLine}";
            text += $"数量：{WaitNum} - {PutNum}{Environment.NewLine}{Environment.NewLine}";
            return text;
        }
    }

    public enum LatticeStatus
    {
        None,
        WaitPut,
        PutError,
        BlockError              
    }

    public class Product
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string ProductCode { get; set; }
        public string PropertyCode { get; set; }
        public string Feature { get; set; }
        public string ProductName { get; set; }
        public int WaitNum { get; set; }
        public int PutNum { get; set; }
        public bool IsComplete => WaitNum - PutNum == 0;
        public ProductStatus Status { get; set; } = ProductStatus.None;
    }

    public enum ProductStatus
    {
        None,
        WaitPut
    }
}
