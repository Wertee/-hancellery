using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using PetProjectMVC.Models.ViewModels;

namespace PetProjectMVC.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private GameStoreIdentityContext _identityContext;

        public AccountController(UserManager<User> userM,SignInManager<User> sign,GameStoreIdentityContext con)
        {
            _userManager = userM;
            _signInManager = sign;
            _identityContext = con;
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

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(string userId)
        {
            var user = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            EditUserVM model = new EditUserVM { UserId = userId, UserName = user.UserName, UserLastName = "none", UserEmail = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            var user = _userManager.Users.Where(x => x.Id == model.UserId).FirstOrDefault();
            user.UserName = model.UserName;
            user.Email = model.UserEmail;
            await _userManager.UpdateAsync(user);
            await _identityContext.SaveChangesAsync();
            return RedirectToAction("UserList", "Admin");
        }
    }
}
