using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SUD.Startup))]
namespace SUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
