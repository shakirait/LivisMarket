
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Data.Helpers
{

    public static class SeedData
    {
        public async static Task Initialize(LivisMarketContext context, UserManager<LevisUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            var isExist = await roleManager.RoleExistsAsync("Admin");
            if (!isExist)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);
            }
            isExist = await roleManager.RoleExistsAsync("Agency");
            if (!isExist)
            {
                var role = new IdentityRole();
                role.Name = "Agency";
                await roleManager.CreateAsync(role);
            }
            isExist = await roleManager.RoleExistsAsync("Supplier");
            if (!isExist)
            {
                var role = new IdentityRole();
                role.Name = "Supplier";
                await roleManager.CreateAsync(role);
            }
            isExist = await roleManager.RoleExistsAsync("Customer");
            if (!isExist)
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                await roleManager.CreateAsync(role);
            }

            isExist = context.Users.Any(u => u.UserName.Equals("Administrator", StringComparison.OrdinalIgnoreCase));
            if (!isExist)
            {
                var user = new LevisUser();
                user.UserName = "Administrator";
                user.Email = "shakirait2007@gmail.com";
                user.PhoneNumber = "01656154722";
                user.PhoneNumberConfirmed = true;
                user.IsPowerUser = true;
                user.EmailConfirmed = true;
                string userPWD = "LivisMarket@2018";
                IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            isExist = context.Users.Any(u => u.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase));
            if (!isExist)
            {
                var user = new LevisUser();
                user.UserName = "Admin";
                user.Email = "lenguyenwi@gmail.com";
                user.IsPowerUser = true;
                user.EmailConfirmed = true;
                string userPWD = "LivisMarket@2018";
                user.PhoneNumber = "00491622792803";
                user.PhoneNumberConfirmed = true;
                IdentityResult chkUser = await userManager.CreateAsync(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result = await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
