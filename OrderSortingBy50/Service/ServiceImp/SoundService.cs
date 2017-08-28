using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service.ServiceImp
{
    public class SoundService
    {
        public void PlaySound(SoundType soundType)
        {
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(GetSoundPath(soundType));
                simpleSound.Play();
            }
            catch { }
        }
        public Task PlaySoundAsny(SoundType soundType)
        {
            try
            {
                return Task.Run(() => PlaySound(soundType));
            }
            catch
            {
                return Task.FromResult(3);
            }
        }

        private string GetSoundPath(SoundType soundType)
        {
            string path = Application.StartupPath + "\\App_Data/Sound\\";
            path += soundType + ".wav";
            return path;
        }   
    }
    public enum SoundType
    {
        //登录成功
        LoginSuccess,
        //未找到波次号
        WaveNotfound,
        //波次完成
        WaveSuccess,
        //该波次已完成
        WaveOverError,
        //开始分拨
        BeginSort,
        //投递出错
        PutError,
        //已解除投递出错
        RemovePutError,
        //波次强制完成
        WavePowerComplete,
        //波次完成
        WaveComplete,
        //阻挡出错
        BlockError,
        //已解除阻挡出错
        RemoveBlockError,
        //未找到产品
        ProductNotFound,
        //中断波次成功
        WaveCancelSuccess,
        //中断波次失败
        WaveCancelError,
        //投递数量出错
        PutNumError,
        //产品已满
        ProductOver,
        //存在待投递的产品
        WaitPut,
        //波次已全部打印
        WavePrintOver,
        //请等待打印完毕
        WatitPrintOver
    }
}
