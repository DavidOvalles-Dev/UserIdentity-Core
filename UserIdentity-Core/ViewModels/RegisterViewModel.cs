using System.ComponentModel.DataAnnotations;

namespace UserIdentity_Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre Completo")]
        public string fullName { get; set; }

        [Required]
        [Display(Name = "Cumpleaños")]
        public DateTime? birthDate { get; set; }

        [Required]
        [Display(Name = "Dirección de email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Confirmar contraseña")]
        [Compare("password", ErrorMessage = "Las contraseñas no coinciden")]
        public string confirmPassword { get; set; }
    }
}
