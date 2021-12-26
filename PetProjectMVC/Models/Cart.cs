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
        public int Id { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
