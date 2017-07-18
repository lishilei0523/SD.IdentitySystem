using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SD.IdentitySystem.Website.Startup))]
namespace SD.IdentitySystem.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
