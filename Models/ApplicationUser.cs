using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreMvcStarter.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
