using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shopping_Cart_01.Startup))]
namespace Shopping_Cart_01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
