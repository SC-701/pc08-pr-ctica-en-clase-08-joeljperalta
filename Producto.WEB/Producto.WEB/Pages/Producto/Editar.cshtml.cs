using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reglas;

namespace Producto.WEB.Pages.Producto
{
    public class EditarModel : PageModel
    {
        private readonly ProductoReglas _productoReglas;

        public EditarModel(ProductoReglas productoReglas)
        {
            _productoReglas = productoReglas;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; } = new();

        public async Task<IActionResult> OnGet(Guid id)
        {
            var prod = await _productoReglas.Obtener(id);
            if (prod == null) return NotFound();

            producto = new ProductoRequest
            {
                Id = prod.Id,
                Nombre = prod.Nombre,
                Descripcion = prod.Descripcion,
                Precio = prod.Precio,
                Stock = prod.Stock,
                CodigoBarras = prod.CodigoBarras,

                IdSubcategoria = prod.IdSubcategoria,
                SubCategoria = prod.IdSubcategoria.ToString(), 
                Categoria = prod.Categoria
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

 
            if (!Guid.TryParse(producto.SubCategoria, out Guid idSubcategoria))
            {
                ModelState.AddModelError("producto.SubCategoria", "Subcategoría inválida");
                return Page();
            }

            producto.IdSubcategoria = idSubcategoria;

            await _productoReglas.Editar(producto.Id, producto);

            return RedirectToPage("./Index");
        }
    }
}