using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class OrderController : Controller // Дописать очистку корзины, после оформления заказа корзина должна очиститься
    {
        private readonly IServiceProvider _services;
        private readonly UserManager<User> _userManager;
        private readonly EFDBContext _context;
        public OrderController(IServiceProvider services, UserManager<User> userM, EFDBContext con)
        {
            _userManager = userM;
            _services = services;
            _context = con;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var cart = Cart.GetCart(_services);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Order order = new Order { UserId = user.Id, Total = cart.CartItems.Sum(x => x.Game.Price * x.Amount) };
            return View(order);
        }

        public async Task<IActionResult> Checkout(Order order)
        {
            if (order != null)
            {
                var cart = Cart.GetCart(_services);

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                List<OrderDetails> listOrderDetails = new List<OrderDetails>();

                foreach (var cartItem in cart.CartItems)
                {
                    var orderDetails = new OrderDetails
                    {
                        OrderId = order.Id,
                        GameId = cartItem.GameId,
                        Amount = cartItem.Amount
                    };
                    _context.OrderDetails.Add(orderDetails);
                    await _context.SaveChangesAsync();
                    listOrderDetails.Add(orderDetails);
                }

                order.OrderDetails = listOrderDetails;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                Cart.ClearCart(_services);

                return RedirectToAction("Index", "Cart");
            }
            return View(order);
        }
    }
}
