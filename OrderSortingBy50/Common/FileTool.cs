using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FileTool
    {
        public byte[] Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在！");
            }
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (IOException ex)
            {
                throw new Exception("读取文件失败：" + ex.ToString());
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}
