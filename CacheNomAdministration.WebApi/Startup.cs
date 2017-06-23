using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CacheNomAdministration.WebApi.Startup))]
namespace CacheNomAdministration.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
