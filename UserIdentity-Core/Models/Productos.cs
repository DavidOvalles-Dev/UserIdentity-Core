using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity_Core.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El Precio es obligatorio.")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }
        public string? UserId { get; set; } // Relación con el usuario

         // Propiedad de navegación para acceder al usuario que publicó el producto
        public virtual ApplicationUser? Usuario { get; set; } // Navegación


    }

}
