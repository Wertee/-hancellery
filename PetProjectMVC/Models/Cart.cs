using PetProjectMVC.EF;
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


        //public void AddGame(Game game,int amount)
        //{
        //    if(game != null)
        //    {
        //        var items = CartItems.Where(x => x.GameId == game.Id).FirstOrDefault();
        //        if(items == null)
        //        {
        //            CartItems.Add(new CartItem() { GameId = game.Id, CartId = Id, Amount = amount });
        //        }
        //        else
        //        {
        //            items.Amount += amount;
        //        }
        //    }
            
        //}
    }
}
