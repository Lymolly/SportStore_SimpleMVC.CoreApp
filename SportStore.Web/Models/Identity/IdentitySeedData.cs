using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Web.Context;

namespace SportStore.Web.Models.Identity
{
    public static class IdentitySeedData
    {
        private const string name = "Admin";
        private const string password = "Password123$";

        public static async void EnsureAdmin(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                user = new IdentityUser(name);
                await userManager.CreateAsync(user,password);
            }
        }
    }
}
