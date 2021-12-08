using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Controllers
{
    public class ProductController:Controller
    {
        private EFDBContext _context;
        public ProductController(EFDBContext con)
        {
            _context = con;
        }

        public IActionResult Index()
        {
            return View(_context.Games.ToList());
        }

        public IActionResult ViewGame(int? id)
        {
            return View(_context.Games.Find(id));
        }
    }
}
