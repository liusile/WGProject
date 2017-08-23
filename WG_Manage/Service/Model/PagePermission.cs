using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public  class PagePermission
    {
        public int RoleID { get; set; }
        public string UserName { get; set; }
        
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
    }
}
