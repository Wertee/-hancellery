

using Microsoft.AspNetCore.Identity;

namespace PetProjectMVC.Models.ViewModels
{
    public class ChangeRoleVM
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }

        public ChangeRoleVM()
        {
            Roles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
