using Microsoft.AspNetCore.Hosting;
using TeamHGSTalentContest.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace TeamHGSTalentContest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}