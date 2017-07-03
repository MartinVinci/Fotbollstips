using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fotbollstips.Startup))]
namespace Fotbollstips
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
