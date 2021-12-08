using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetProjectMVC.Models;

namespace PetProjectMVC.EF
{
    public class GameStoreIdentityContext:IdentityDbContext<User>
    {
        public GameStoreIdentityContext(DbContextOptions<GameStoreIdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
