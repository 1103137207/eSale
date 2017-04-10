using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eSale.Startup))]
namespace eSale
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
