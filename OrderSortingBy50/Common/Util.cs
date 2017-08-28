using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Util
    {
        public static string ClipString(string inputString, int len)
        {
            if (inputString==null || inputString.Length <= len)
            {
                return inputString;
            }
            return inputString.Substring(0, len) + "...";
        }
      
    }
}
