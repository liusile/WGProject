using Domain.Models;
using Service.Model;
using Service.ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Service.IService
{
    public interface IUpDownService
    {
        //登录成功
        void LoginSuccess(UpDownMessage upDownMessage); 
        //开始分播
        void BeginSort(SlaveInfo slaveInfo);
        //已分拨
        void WaveOverError(string waveNo);
        //批次不存在
        void WaveNotfound(UpDownMessage upDownMessage);
        //待投递
        void WaitPut(List<LatticeInfo> latticeInfoList,string barCode);
        //已投递
        void PutIng(UpDownMessage upDownMessage);
        //投递出错
        void PutError(UpDownMessage upDownMessage);
        //解除投递出错
        void RemovePutError(UpDownMessage upDownMessage);
        void SetSocket(Socket socket);

        //投递正确
        void PutSuccess(UpDownMessage upDownMessage);
        //格口已满
        void LatticeComplete(UpDownMessage upDownMessage);
        //阻挡
        void BlockError(UpDownMessage upDownMessage);
        void BlockErrorSuccess(UpDownMessage upDownMessage);
        //解除阻挡
        void RemoveBlockError(UpDownMessage upDownMessage);
        void RemoveBlockErrorSuccess(UpDownMessage upDownMessage);
        //单个打印
        void PrintSingle(UpDownMessage upDownMessage);
        //全部打印
        void PrintAll(UpDownMessage upDownMessage);
        //分播中断
        void WaveCancel(UpDownMessage upDownMessage);
        //分播中断成功
        void WaveCancelSuccess(UpDownMessage upDownMessage);
        //分播中断失败
        void WaveCancelError(UpDownMessage upDownMessage);
        //分拨完成
        void WaveComplete(UpDownMessage upDownMessage);
        //强制完成
        void WavePowerComplete(UpDownMessage upDownMessage);
        //批次中未找到产品
        void ProductNotFound(UpDownMessage upDownMessage);
        //投递出错
        void PutNumError(UpDownMessage upDownMessage);
        //设置文字
        void SetText(UpDownMessage upDownMessage);
        //强制完成成功
        void WavePowerCompleteSuccess(UpDownMessage upDownMessage);
        //打印完毕
        void WavePrintOver(UpDownMessage upDownMessage);
    }
}
