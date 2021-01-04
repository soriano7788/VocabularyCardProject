using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(VocabularyCard.Web.Startup))]
namespace VocabularyCard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
