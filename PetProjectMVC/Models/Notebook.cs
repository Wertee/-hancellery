using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.Models
{
    public class Notebook:Product
    {
        [Required]
        public int ListCount { get; set; }
    }
}
