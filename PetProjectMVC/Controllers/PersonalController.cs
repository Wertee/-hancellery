using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Controllers
{
    public class PersonalController : Controller
    {
        private UserManager<User> _userManager;
        private EFDBContext _context;
        public PersonalController(UserManager<User> userM, EFDBContext con)
        {
            _userManager = userM;
            _context = con;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }

        public async Task<IActionResult> PersonalOrders()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var orders = _context.Orders.Include(t => t.OrderDetails).ThenInclude(t => t.Game).Where(x => x.UserId == user.Id).ToList();
            return View(orders);
        }
    }
}
