using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System.Diagnostics;

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
            return View();
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