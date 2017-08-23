using System.Web.Mvc;

namespace WG_BackStage.Areas.HeadStage
{
    public class HeadStageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HeadStage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "HeadStage_default",
                "HeadStage/{controller}/{action}/{id}",
                new { Controller="Home",action = "Index", id = UrlParameter.Optional },
                new string[] { "WG_BackStage.Areas.HeadStage.Controllers" }
            );
        }
    }
}