using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SportStore.Web.Models.Identity;

namespace SportStore.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signinManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await userManager.FindByNameAsync(model.Name);
                if (identityUser != null)
                {
                    await signinManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult res = await signinManager
                        .PasswordSignInAsync(identityUser, model.Password,false,false);
                    if (res.Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Incorrect name or password!");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut(string returnUrl = "/")
        {
            await signinManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
