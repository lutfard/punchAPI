using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(punchAPI.Startup))]
namespace punchAPI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
