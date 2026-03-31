using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Producto.WEB.Pages.Producto
{
    public class EliminarModel : PageModel
    {
        private readonly ProductoReglas _productoReglas;

        public EliminarModel(ProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;

        public async Task<IActionResult> OnGet(Guid id)
        {
            producto = await _productoReglas.Obtener(id);
            if (producto == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await _productoReglas.Eliminar(producto.Id);
            return RedirectToPage("./Index");
        }
    }
}