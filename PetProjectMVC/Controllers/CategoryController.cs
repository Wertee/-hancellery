using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController:Controller
    {
        private readonly EFDBContext _context;
        public CategoryController(EFDBContext con)
        {
            _context = con;
        }

        public IActionResult Index()
        {
            return View(_context.Categories.ToList());
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult EditCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if(category != null)
            {
                return View(category);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);


        }
    }
}
