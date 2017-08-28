using BarcodeLib;
using System;
using System.Drawing;
using System.Drawing.Printing;
using Domain.Models;
using System.Collections.Generic;

namespace Service
{
    public class CustomPrint : PrintDocument
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
                e.Graphics.DrawImage(b.Encode(type, LatticeInfo.OrderNo, 275, 60), 20, 70);
                e.Graphics.DrawString(LatticeInfo.OrderNo, Regularfont, Brushes.Black, 90, 133);
                e.Graphics.DrawString("格口：" + LatticeInfo.LatticeNo, Regularfont, Brushes.Black, x, 162);
                e.Graphics.DrawString("数量：" + LatticeInfo.PutNum, Regularfont, Brushes.Black, x, 192);
                e.Graphics.DrawString("日期：" + DateTime.Now.ToString("yyyy/MM/dd"), Regularfont, Brushes.Black, x, 222);
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
