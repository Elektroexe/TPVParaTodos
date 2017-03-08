using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebService.Startup))]
namespace WebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
