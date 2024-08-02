using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StevenDavidPillay_162218218_Test4.Startup))]

namespace StevenDavidPillay_162218218_Test4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
