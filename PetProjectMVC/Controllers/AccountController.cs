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
                User user = await _userManager.FindByEmailAsync(loginModel.UserEmail);
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
                            var roles = await _userManager.GetRolesAsync(user);
                            var isAdmin = roles.FirstOrDefault(x=>x=="Admin")?.Select(x=>x);
                            if (isAdmin!=null)
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Product");
                            }
                            
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
        public async Task<IActionResult> Register(CreateUserVM createUserVM)
        {
            //User user = new User { Email = "Pavlov@mail.ru", UserName = "Admin" };
            //var result = await _userManager.CreateAsync(user, "Fb4a6a22_a");

            User user = new User { Email = createUserVM.UserEmail };
            user.UserName = createUserVM.UserEmail;
            var result = await _userManager.CreateAsync(user, createUserVM.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Ошибка создания пользователя");
            return View(createUserVM);
            
        }
    }
}
