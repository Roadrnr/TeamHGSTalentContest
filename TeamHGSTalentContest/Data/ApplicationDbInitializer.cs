using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TeamHGSTalentContest.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@site.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@site.com",
                    Email = "admin@site.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    //userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
