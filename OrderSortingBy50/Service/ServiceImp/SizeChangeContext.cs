using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.IService;

namespace Service.ServiceImp
{
    /// <summary>
    /// 
    /// </summary>
    public class SizeChangeContext:AbstractSizeChangeContext
    {
        public int LineCount { get; set; }
        public int FormWidth { get; set; }
        public int FormHeight { get; set; }
        public int RowCount { get; private set; }

        private int _xAdd;

        private int _yAdd;
        public SizeChangeContext(int width,int height,int lineCount,int rowCount)
        {
            this.LineCount = lineCount;
            this.RowCount = rowCount;
            FormWidth = width;
            FormHeight=height;
            InitButtonLocationn();
        }
        internal sealed override void InitButtonLocationn()
        {
            _columnCount = 1;
            _x = _startX1 = 0;//起始位置的坐标的x的值
            _y = _startY1 =  80;//起始位置的坐标的y的值
            _btnWidth = Convert.ToInt32((FormWidth-10) /LineCount);
            _btnHeight = Convert.ToInt32((FormHeight-150) / RowCount);
            _emSize = (float)((FormWidth + FormHeight) * 0.0055);
            _xAdd = Convert.ToInt32(_btnWidth);
            _yAdd = Convert.ToInt32(_btnHeight);
        }

        internal override void UpdateButtonLocation()
        {
            _x += _xAdd;//每装载下一个button使其x坐标增加
            _columnCount += 1;
            if (_columnCount > LineCount)
            {
                _columnCount = 1;
                _x = _startX1;
                _y += _yAdd;
            }
        }
    }
}
