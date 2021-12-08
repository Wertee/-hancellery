using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.Models;
using PetProjectMVC.Models.ViewModels;

namespace PetProjectMVC.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userM,SignInManager<User> sign)
        {
            _userManager = userM;
            _signInManager = sign;
        }

        public IActionResult Login(string returnURL)
        {
            return View(new LoginModel { ReturnUrl=returnURL});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        if (loginModel.ReturnUrl != null)
                        {
                            return Redirect(loginModel.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                }
            }
            else 
            {
                ModelState.AddModelError("", "Wrong username or password");
            }

            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string ret = "/")
        {
            User user = new User { Email = "Pavlov@mail.ru", UserName = "Admin" };
            var result = await _userManager.CreateAsync(user, "Fb4a6a22_a");
            return RedirectToAction("Login");
        }
    }
}
