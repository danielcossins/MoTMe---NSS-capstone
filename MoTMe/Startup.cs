using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoTMe.Startup))]
namespace MoTMe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
