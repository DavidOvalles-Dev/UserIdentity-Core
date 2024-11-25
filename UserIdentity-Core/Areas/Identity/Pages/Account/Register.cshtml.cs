using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UserIdentity_Core.Models;

namespace UserIdentity_Core.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        // Modelo de vista InputModel - contiene solo las propiedades para la vista
        public class InputModel
        {
            [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
            [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "El nombre completo es obligatorio.")]
            [Display(Name = "Nombre Completo")]
            public string fullName { get; set; }



        }


        // Acción para obtener la página de registro
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        // Acción cuando el formulario es enviado (PO  ST)
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            Console.WriteLine(ModelState);

            // Si el modelo es válido, se crea el usuario
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Email: {Input.Email}");
                Console.WriteLine($"Password: {Input.Password}");
                Console.WriteLine($"ConfirmPassword: {Input.ConfirmPassword}");
                Console.WriteLine($"FullName:{Input.fullName }");
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    fullName = Input.fullName,

                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Inicia sesión automáticamente al registrarse
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl ?? Url.Content("~/"));
                }


                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page(); // Vuelve a la página con los errores mostrados
                }

                // Si hubo errores, los añadimos al modelo
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error en '{key}': {error.ErrorMessage}");
                    }
                }
                return Page(); // Retorna la misma página para mostrar los errores
            }
            return Page(); // Retorna la página si no se puede registrar
        }
    }

}
