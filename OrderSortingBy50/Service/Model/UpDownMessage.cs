using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    /// <summary>
    /// 上下位机通讯模型
    /// </summary>
    public  class UpDownMessage
    {
        public string Type { get; set; }
        public UpDownCommand UpDownCommand { get; set; }
        public List<LatticeByUpDown> LatticeByUpDown { get; set; }
        public string Message { get; set; }
    }

    public class LatticeByUpDown
    {
        public string LatticeNo { get; set; }
        public int Num { get; set; }
    }
     
    public enum UpDownCommand
    {
        //登录成功
        LoginSuccess,
        //开始分播
        BeginSort,
        //已分拨
        WaveOverError,
        //批次不存在
        WaveNotfound,
        //待投递
        WaitPut,
        //已投递
        PutIng,
        //投递出错
        PutError,
        //解除投递出错
        RemovePutError,
        //投递正确
        PutSuccess,
        //格口已满
        LatticeComplete,
        //阻挡
        BlockError,
        //解除阻挡
        RemoveBlockError,
        //单个打印
        PrintSingle,
        //全部打印
        PrintAll,
        //分播中断
        WaveCancel,
        //分拨完成
        WaveComplete,
        //强制完成
        WavePowerComplete,
        ProductNotFound,
        //分播中断成功
        WaveCancelSuccess,
        //分播中断失败
        WaveCancelError,
        //投递数量出错
        PutNumError,
        //设置文字
        SetText,
        //阻挡成功
        BlockErrorSuccess,
        //解除阻挡成功
        RemoveBlockErrorSuccess,
        WavePowerCompleteSuccess,
        WavePrintOver
    }
}
