using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CardinalHub.Startup))]
namespace CardinalHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
