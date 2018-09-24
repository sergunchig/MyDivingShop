using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyDivingShop2a.Startup))]
namespace MyDivingShop2a
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
