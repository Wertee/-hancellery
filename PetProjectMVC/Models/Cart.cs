using PetProjectMVC.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> CartItems = new List<CartItem>();

        private readonly EFDBContext _context;
        private IServiceProvider _services;

        public Cart(EFDBContext con)
        {
            _context = con;
        }
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<EFDBContext>();
            var count = context.CartItems.Select(x => x.CartId).Distinct().Count();
            string cartId = session.GetString("CartId") ?? (++count).ToString();
            session.SetString("CartId", cartId);
            var cartItems = context.CartItems.Include(g=>g.Game).Where(x => x.CartId == cartId).ToList();
            return new Cart(context) { Id = cartId, CartItems = cartItems };
        }
    }
}
