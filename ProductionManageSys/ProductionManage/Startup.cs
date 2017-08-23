using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProductionManage.Startup))]
namespace ProductionManage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
