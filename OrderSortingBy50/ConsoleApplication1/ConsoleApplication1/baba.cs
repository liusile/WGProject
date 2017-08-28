using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class baba
    {
        string name = "baba";
    }
    class son :baba
    {
        string name = "son";
        public new Type GetType()
        {
            return base.GetType();
        }
    }
}
