using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class LoginRequestContract
    {
        public string LoginName { get; internal set; }
        public string Password { get; internal set; }
        public string Token { get; internal set; }
    }
}
