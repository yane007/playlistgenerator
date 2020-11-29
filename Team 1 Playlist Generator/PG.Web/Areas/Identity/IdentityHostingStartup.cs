using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(PG.Web.Areas.Identity.IdentityHostingStartup))]
namespace PG.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}