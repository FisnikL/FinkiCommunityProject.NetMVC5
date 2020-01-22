using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinkiCommunity.Startup))]
namespace FinkiCommunity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
