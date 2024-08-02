using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDP2241A5.Startup))]

namespace SDP2241A5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
