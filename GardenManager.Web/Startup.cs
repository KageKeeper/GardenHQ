using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GardenManager.Web.Startup))]
namespace GardenManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
