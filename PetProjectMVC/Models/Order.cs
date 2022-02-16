using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
