using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VirtualLockerAPI.DTO_s
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Por favor, ingresar el nombre del producto."), MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "La marca es obligatoria."), MaxLength(100)]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "Debes Seleccionar una categoria."), MaxLength(100)]
        public string Category { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFileName { get; set; }
    }
}
