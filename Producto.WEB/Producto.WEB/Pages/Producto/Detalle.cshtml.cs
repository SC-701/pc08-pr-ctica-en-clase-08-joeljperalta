using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Producto.WEB.Pages.Producto
{
    public class DetalleModel : PageModel
    {
        private readonly ProductoReglas _productoReglas;
        public ProductoResponse producto { get; set; } = default!;

        public DetalleModel(ProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            producto = await _productoReglas.Obtener(id);
            if (producto == null) return NotFound();
            return Page();
        }
    }
}