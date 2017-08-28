using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.IService;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Domain.Models;
using Service.Model;

namespace Service.ServiceImp
{
    public class ButtonService : IButtonService
    {
        Image greenLight = Image.FromFile(Application.StartupPath+"/Img/GreenCircle.png");
        Image grayLight = Image.FromFile(Application.StartupPath + "/Img/GrayCircle.png");
        private ButtonService() { }
        public ButtonService(SlaveInfoService slaveInfoService)
        {
            this.SlaveInfoService = slaveInfoService;
        }
        public SlaveInfoService SlaveInfoService { get;  set; }
        public List<Button> BtnList { get; private set; }

        private  Button CreateButton(LatticeInfo latticeInfo, AbstractSizeChangeContext sizeContext)
        {
            var btn = new Button()
            {
                Name = latticeInfo.LatticeNo,
                Text = latticeInfo.DisPlay(), 
                TextAlign=ContentAlignment.MiddleLeft,
                Location = new Point(sizeContext.X, sizeContext.Y),
                Size = new Size(sizeContext.BtnWidth, sizeContext.BtnHeight),
                BackColor= latticeInfo.BackColor(),
                Font = new Font("宋体", sizeContext.EmSize, FontStyle.Bold, GraphicsUnit.Point, 134)
            };
            sizeContext.UpdateButtonLocation();
            return btn;
        }
        public virtual List<Button>  CreateButtonList (AbstractSizeChangeContext sizeContext)
        {
            List<Button> btnList = new List<Button>();
            SlaveInfoService.LatticeInfoList.ForEach(latticeInfo =>
            {
                var btn=CreateButton(latticeInfo, sizeContext);
                btnList.Add(btn);
            });
            return btnList;
        }
        
        public virtual void UpdateButton(LatticeInfo latticeInfo)
        {
            var SlavelatticeInfo = SlaveInfoService.LatticeInfoList.First(o => o.LatticeNo == latticeInfo.LatticeNo);
            var btn = BtnList.FirstOrDefault(o => o.Name == SlavelatticeInfo.LatticeNo);
            if (btn != null) { 
                btn.Text = SlavelatticeInfo.DisPlay();
                btn.BackColor = SlavelatticeInfo.BackColor();
                btn.ImageAlign = System.Drawing.ContentAlignment.TopRight;
                btn.Image = latticeInfo.IsComplete&&latticeInfo.PutNum>0 ? greenLight : grayLight;
            }
        }
        public virtual void GreenAll()
        {
            Parallel.ForEach(BtnList, o => o.BackColor = Color.Green);
        }
        public virtual void GainsboroAll()
        {
            Parallel.ForEach(BtnList, o => o.BackColor = Color.Gainsboro);
        }
        public virtual void UpdateButton(UpDownMessage upDownMessage)
        {
            UpdateButton(SlaveInfoService.GetLatticeInfoList(upDownMessage));
        }
        public virtual void UpdateButton(List<LatticeInfo> latticeInfoList)
        {
            latticeInfoList.ForEach(UpdateButton);
        }
        public void SetButtonList(List<Button> btnList)
        {
            this.BtnList = btnList;
        }
        public virtual void UpdateButtons()
        {
            SlaveInfoService.LatticeInfoList.ForEach(UpdateButton);
        }
        public void ReSizeButons(AbstractSizeChangeContext sizeContext)
        {
            var btnList = CreateButtonList(sizeContext);
            if (btnList != null)
            {
                BtnList.ForEach(btn =>
                {
                    var bpc = btnList.Find(o => o.Name == btn.Name);
                    btn.Location = bpc.Location;
                    btn.Size = bpc.Size;
                    btn.Font = bpc.Font;
                });
            }
        }
    }
}
