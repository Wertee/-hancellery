using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System.Diagnostics;
using System.IO;

namespace PetProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EFDBContext _context;
        public HomeController(EFDBContext con,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = con;
        }

        public IActionResult Index()
        {
            return View(_context.Games.ToList());
        }
        [HttpGet]
        public IActionResult EditGame(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            return View(_context.Games.Find(id));
        }

        [HttpPost]
        public IActionResult EditGame(Game game, IFormFile uploadImage)
        {
            if (ModelState.IsValid)
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
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}