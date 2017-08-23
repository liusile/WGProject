using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WG_BackStage.Areas.BackStage;

namespace WG_BackStage
{
    public class FilterConfig
    {
        internal static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new UserAuthorizeAttribute());
        }
    }
}