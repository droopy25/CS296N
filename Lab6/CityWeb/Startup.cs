using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CityWeb.Startup))]
namespace CityWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
