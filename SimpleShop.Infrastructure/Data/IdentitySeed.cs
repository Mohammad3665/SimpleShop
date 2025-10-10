using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Data
{
    public static class IdentitySeed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User" };

            var isExistAdminRole = await roleManager.RoleExistsAsync(roleNames[0]);
            var isExistUserRole = await roleManager.RoleExistsAsync(roleNames[1]);

            if (!isExistAdminRole)
            {
                var role = new ApplicationRole()
                {
                    Name = roleNames[0]
                };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.Select(e => e.Description).First());
            }
            if (!isExistUserRole)
            {
                var role = new ApplicationRole()
                {
                    Name = roleNames[1]
                };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.Select(e => e.Description).First());
            }

            var defaultAdminEmail = "mashmammad876@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(defaultAdminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = defaultAdminEmail,
                    Email = defaultAdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
            
        }

    }
}
