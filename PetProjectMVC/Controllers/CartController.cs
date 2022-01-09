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
            Cart cart = Cart.GetCart(_services);
            return View(cart);
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


        //public Cart GetCart()
        //{
        //    ISession session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        //    var context = _services.GetService<EFDBContext>();
        //    var count = _context.CartItems.Select(x => x.CartId).Distinct().Count();
        //    var ccc = session.GetString("CartId");
        //    string cartId = session.GetString("CartId") ?? (++count).ToString();
        //    session.SetString("CartId", cartId);
        //    var cartItems = _context.CartItems.Where(x => x.CartId == cartId).ToList();
        //    return new Cart { Id = cartId, CartItems = cartItems };
        //}

        public void AddGame(Game game, int amount)
        {
            var cart = Cart.GetCart(_services);
            
            if (game != null)
            {
                var item = cart.CartItems.Where(x => x.GameId == game.Id).FirstOrDefault();
                if (item == null)
                {
                    _context.CartItems.Add(new CartItem() { GameId = game.Id, CartId = cart.Id, Amount = amount});
                    
                }
                else
                {
                    item.Amount += amount;
                    _context.CartItems.Update(item);
                    
                }
                _context.SaveChanges();
            }
        }

        public IActionResult RemoveFromCart(int gameId)
        {
            var cart = Cart.GetCart(_services);
            var removedItem = _context.CartItems.Where(x => x.GameId == gameId && x.CartId == cart.Id).FirstOrDefault();
            if(removedItem != null)
            {
                _context.CartItems.Remove(removedItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
            

        }


    }
}
