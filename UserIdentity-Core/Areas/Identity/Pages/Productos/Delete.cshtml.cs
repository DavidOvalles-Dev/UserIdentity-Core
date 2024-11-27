using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserIdentity_Core.Data;
using UserIdentity_Core.Models;

namespace UserIdentity_Core.Areas.Identity.Pages.Productos
{
    public class DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        [BindProperty]
        public Producto Producto { get; set; }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Busca el producto por ID
           var Producto = await _context.Productos
                .AsNoTracking() // Solo lectura, no rastrear
                .FirstOrDefaultAsync(p => p.Id == id);

            if (Producto == null)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Producto == null || Producto.Id == 0)
            {
                return NotFound();
            }

            var productoToDelete = await _context.Productos.FindAsync(Producto.Id);

            if (productoToDelete == null)
            {
                return NotFound();
            }

            // Verifica que el usuario sea el propietario
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtiene el ID del usuario autenticado
            if (productoToDelete.UserId != userId)
            {
                return Forbid(); // Retorna 403 si no es el propietario
            }

            _context.Productos.Remove(productoToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


    }

}

