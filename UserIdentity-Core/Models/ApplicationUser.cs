using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserIdentity_Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nombre Completo")]
        public string fullName { get; set; }

    }
}
