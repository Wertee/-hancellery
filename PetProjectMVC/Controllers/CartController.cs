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
    public class CartController:Controller
    {
        private readonly EFDBContext _context;
        public CartController(EFDBContext con)
        {
            _context = con;
        }

        public IActionResult Index()
        {
            return View();
        }
     
        public IActionResult AddToCart(int id)
        {
            var game = _context.Games.Find(id);

            if(game == null)
            {
                return RedirectToAction("Home", "Index");
            }

            if (!_context.Carts.Any())
            {
                Cart cart = new Cart();
                cart.CartId = 1;
                cart.Count = 1;
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            else
            {
                Cart cart = _context.Carts.Find(1);
                cart.Count += 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Product");
        }


    }
}
