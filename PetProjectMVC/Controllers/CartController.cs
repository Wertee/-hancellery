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
    public class CartController:Controller
    {
        private readonly EFDBContext _context;
        private IServiceProvider _services;
        public CartController(EFDBContext con, IServiceProvider services)
        {
            _context = con;
            _services = services;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View();
        }
     
        public IActionResult AddToCart(int id)
        {
            Game game = _context.Games.Find(id);
            if(game != null)
            {
                AddGame(game, 1);
            }
            return RedirectToAction("Index", "Product");
        }


        public Cart GetCart()
        {
            ISession session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = _services.GetService<EFDBContext>();
            var count = _context.CartItems.Select(x => x.CartId).Distinct().Count(); //поиск сколько всего корзин
            var ccc = session.GetString("CartId");
            string cartId = session.GetString("CartId") ?? (++count).ToString();
            session.SetString("CartId", cartId);
            return new Cart { Id = cartId };
        }

        public void AddGame(Game game, int amount)
        {
            var cart = GetCart();
            var cartItems = _context.CartItems.Where(x => x.CartId == cart.Id).ToList();
            if (game != null)
            {
                var item = cartItems.Where(x => x.GameId == game.Id).FirstOrDefault();
                if (item == null)
                {
                    _context.CartItems.Add(new CartItem() { GameId = game.Id, CartId = cart.Id, Amount = amount });
                    
                }
                else
                {
                    item.Amount += amount;
                    _context.CartItems.Update(item);
                    
                }
                _context.SaveChanges();
            }
        }


    }
}
