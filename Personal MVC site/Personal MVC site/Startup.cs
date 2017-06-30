using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Personal_MVC_site.Startup))]
namespace Personal_MVC_site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
