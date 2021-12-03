using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetProjectMVC.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        public int Stock { get; set; }
        
        public byte[]? Image { get; set; }

        public decimal Price { get; set; }

        public string? Category { get; set; }

    }
}
