using Microsoft.AspNetCore.Identity;

namespace PetProjectMVC.Models
{
    public class User : IdentityUser
    {
        public string UserLastName { get; set; }
    }
}
