using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TeamHGSTalentContest.Data
{
    public class ApplicationDbInitializer
    {
        public static async Task SeedData
        (UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole {Name = "Admin", NormalizedName = "Admin".ToUpper()});
            }

            if (await userManager.FindByEmailAsync("admin@site.com") != null) return;

            var user = new IdentityUser
            {
                UserName = "admin@site.com",
                Email = "admin@site.com"
            };

            var result = await userManager.CreateAsync(user, "P@ssw0rd");

            if (result.Succeeded)
            {
                await userManager.AddPasswordAsync(user, "P@ssw0rd");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.RoleExistsAsync("Admin")) return;

            var role = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            };

            await roleManager.CreateAsync(role);
        }
        public static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@site.com") != null) return;

            var user = new IdentityUser
            {
                UserName = "admin@site.com",
                Email = "admin@site.com"
            };

            var result = (await userManager.CreateAsync(user, "P@ssw0rd")).Succeeded;

            if (result)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
