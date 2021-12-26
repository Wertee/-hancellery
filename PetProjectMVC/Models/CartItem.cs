using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models
{
    public class CartItem
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int Amount { get; set; }
        public int CartId { get; set; }
    }
}
