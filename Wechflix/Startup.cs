using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wechflix.Startup))]
namespace Wechflix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
