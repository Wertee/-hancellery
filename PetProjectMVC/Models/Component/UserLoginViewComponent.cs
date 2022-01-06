using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models.Component
{
    public class UserLoginViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserLoginViewComponent(UserManager<User> userM)
        {
            _userManager = userM;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View("_UserLoginComponent", user);
        }
    }
}
