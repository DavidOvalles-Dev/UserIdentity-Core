using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using UserIdentity_Core.Data;
using UserIdentity_Core.Models;

namespace UserIdentity_Core.Areas.Identity.Pages.Productos
{
    [Authorize]
    public class MisProductosModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Producto> Productos { get; set; }

        public MisProductosModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Productos = _context.Productos.Where(p => p.UserId == userId).ToList();           

        }
    }
}
