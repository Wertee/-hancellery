using Microsoft.AspNetCore.Mvc;
using PetProjectMVC.Controllers;
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
        private IServiceProvider _services;
        public CartViewComponent(EFDBContext con, IServiceProvider services)
        {
            _context = con;
            _services = services;
        }
        public IViewComponentResult Invoke()
        {
            Cart cart = Cart.GetCart(_services);
            return View("_CartPartial",cart);
        }
    }
}
