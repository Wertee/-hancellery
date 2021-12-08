using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectMVC.EF
{
    public class EFDBContext:DbContext
    {
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
        {
            
            Database.EnsureCreated();
            //Games.Add(new Game { Name = "Ведьмак", Category = "RPG", Description="Ролевая игра с видом от третьего лица",Stock = 50,Price=1999M });
            //Games.Add(new Game { Name = "World of Warcraft", Category = "MMO", Description = "Ролевая игра с видом от третьего лица", Stock = 50, Price = 999M });
            //Games.Add(new Game { Name = "Battlefield 3099", Category = "Shooter", Description = "Ролевая игра с видом от третьего лица", Stock = 50, Price = 3600M });
            //this.SaveChanges();
        }

        public DbSet<Game> Games { get; set; }

    }
}
