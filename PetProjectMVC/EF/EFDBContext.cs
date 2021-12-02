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
            Pens.Add(new Pen { Name = "Krauser", Description = "014ASD5", Color = "Blue",Stock=10 });
            this.SaveChanges();
            Pens.Add(new Pen { Name = "Corvina", Description = "Cor1", Color = "Blue", Stock = 5 });
            this.SaveChanges();
            Pencils.Add(new Pencil { Name = "SuperPen", Description = "SG1", Color = "Red", Stock = 3 });
            this.SaveChanges();
            Notebooks.Add(new Notebook { Name = "Mimimishka", Description = "SB21", Stock = 6,ListCount=64 });
            this.SaveChanges();
        }

        DbSet<Pen> Pens { get; set; }
        DbSet<Pencil> Pencils { get; set; }
        DbSet<Notebook> Notebooks { get; set; }
    }
}
