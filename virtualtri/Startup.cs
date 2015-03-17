using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(virtualtri.Startup))]
namespace virtualtri
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
