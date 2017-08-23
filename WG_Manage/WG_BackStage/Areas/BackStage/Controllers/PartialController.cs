using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WG_BackStage.Areas.BackStage.Controllers
{
    public class PartialController :BaseController
    {
        // GET: BackStage/Partial
        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult PartialHead()
        {
            return PartialView(this.CurrentUser);
        }
    }
}