using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefualtAdminUser
    {
        public static async void SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seeds
            ApplicationUser defualtUser = new ApplicationUser
            {
                UserName = "UserAdmin",
                Email = "UserAdmin@mail.com",
                Name = "Juan Diego",
                LastName = "Aguilar",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if(userManager.Users.All(u => u.Id != defualtUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defualtUser.Email);
                if(user == null)
                {
                    await userManager.CreateAsync(defualtUser, "123Pa$sword");
                    await userManager.AddToRoleAsync(defualtUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defualtUser, Roles.basic.ToString());
                }
            }
        }
    }
}
