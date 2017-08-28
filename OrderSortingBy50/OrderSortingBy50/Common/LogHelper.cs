using System;
using System.Threading.Tasks;
using log4net;

namespace OrderSortingBy50.Common
{
    public static class LogHelper
    {
        static ILog log = LogManager.GetLogger("log");

        public static void Save(string content,Exception ex)
        {
            log.Error(content, ex);
        }
        public static void Save(string content)
        {
            log.Error(content);
        }
        public static void SaveAsyn(string content)
        {
            Task.Run( ()=> log.Error(content));
        }
        public static void SaveAsyn(string content,Exception ex)
        {
            Task.Run(() => log.Error(content, ex));
        }
    }
}
