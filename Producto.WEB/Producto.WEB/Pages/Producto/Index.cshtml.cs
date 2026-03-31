using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;
using System.Text.Json;

namespace Producto.WEB.Pages.Producto
{
    public class IndexModel : PageModel
    {
        private readonly ProductoReglas _productoReglas;
        public List<ProductoResponse> productos { get; set; } = new();

        public IndexModel(ProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        public async Task OnGet()
        {
            productos = await _productoReglas.Obtener();
        }
    }
}