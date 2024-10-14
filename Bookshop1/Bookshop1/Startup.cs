using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookshop1.Startup))]
namespace Bookshop1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
