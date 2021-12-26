using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System.Diagnostics;
using System.IO;

namespace PetProjectMVC.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly EFDBContext _context;
        public AdminController(EFDBContext con,ILogger<AdminController> logger)
        {
            _logger = logger;
            _context = con;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View(_context.Games.Include(c => c.Category).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddGame()
        {
            SelectList categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddGame(Game game, IFormFile uploadImage)
        {
                if (uploadImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadImage.CopyTo(ms);
                        game.Image = ms.ToArray();
                    }
                }
                _context.Games.Add(game);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditGame(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            SelectList categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Categories = categories;

            return View(_context.Games.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditGame(Game game, IFormFile uploadImage)
        {

                if(uploadImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        uploadImage.CopyTo(ms);
                        game.Image = ms.ToArray();
                    }
                }
                _context.Games.Update(game);
                _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}