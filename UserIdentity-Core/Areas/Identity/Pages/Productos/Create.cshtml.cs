using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UserIdentity_Core.Data;
using UserIdentity_Core.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace UserIdentity_Core.Areas.Identity.Pages.Productos
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Producto Producto { get; set; }

        [BindProperty]
        public IFormFile? Imagen { get; set; } // Para manejar la imagen subida

        public async Task<IActionResult> OnPostAsync()
        {



            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($"Error en '{modelStateKey}': {error.ErrorMessage}");
                    }
                }
                return Page();
            }


            // Asigna el UserId al producto
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Producto.UserId = userId;

            // Guardar el producto en la base de datos
            _context.Productos.Add(Producto);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Productos/MisProductos");
        }
    }
}
