using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nlevel.Web.Startup))]
namespace Nlevel.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
