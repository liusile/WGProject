using BarcodeLib;
using System;
using System.Drawing;
using System.Drawing.Printing;
using Domain.Models;
using System.Collections.Generic;

namespace Service
{
    public class ProductPrint : PrintDocument
    {
        private LatticeInfo LatticeInfo;
        Barcode b = new Barcode();
        TYPE type = TYPE.CODE128;

        public void PrintSetup(LatticeInfo latticeInfo)
        {
            this.LatticeInfo = latticeInfo;
            DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            //页面设置
            DefaultPageSettings.PaperSize = new PaperSize("printProcess", 320, 320);
            Print();
        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            try
            {   
                b.IncludeLabel = false;
                Font Regularfont = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
                var x = 60;
                var y = 1;
                e.Graphics.DrawString("缺货信息如下：", Regularfont, Brushes.Black, 20, 20 * y++);
                this.LatticeInfo.Product
                    .FindAll(o => !o.IsComplete)
                    .ForEach(o =>
                    {
                        var productMsg = o.BarCode + "缺" + (o.WaitNum - o.PutNum) + "件";
                        e.Graphics.DrawString("订单："+o.ProductName, Regularfont, Brushes.Black, 20, 20 * y++);
                        e.Graphics.DrawString(productMsg, Regularfont, Brushes.Black, x, 20 * y++);
                        e.Graphics.DrawImage(b.Encode(type, o.BarCode, 260, 60), 20, 20 * y+70);
                    });
                e.HasMorePages = false;
                Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
