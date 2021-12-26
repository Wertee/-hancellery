using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.EF;
using PetProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models.Component
{
    public class CartViewComponent:ViewComponent
    {
        private EFDBContext _context;
        public CartViewComponent(EFDBContext con)
        {
            _context = con;
        }
        public IViewComponentResult Invoke()
        {
            Cart cart = _context.Carts.Find(1);
            if(cart == null)
            {
                return View("_CartPartial", new Cart() { CartId = 1, ProductId = 1, Count = 1 });
            }
            return View("_CartPartial", cart);
        }
    }
}
