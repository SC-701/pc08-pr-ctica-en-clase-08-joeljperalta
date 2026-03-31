using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Producto.WEB.Pages.Producto
{
    [Authorize]
    public class EditarModel : PageModel
    {
        private readonly IConfiguration _configuracion;

        public EditarModel(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; } = new();

        public async Task<IActionResult> OnGet(Guid id)
        {
            string endpoint = _configuracion["ApiProductos"] + $"Producto/{id}";

            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.GetAsync(endpoint);

            if (!respuesta.IsSuccessStatusCode)
                return NotFound();

            var resultado = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var prod = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

            if (prod == null)
                return NotFound();

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

            string endpoint = _configuracion["ApiProductos"] + $"Producto/{producto.Id}";

            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.PutAsJsonAsync(endpoint, producto);

            respuesta.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }

        private HttpClient ObtenerClienteConToken()
        {
            var tokenClaim = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == "Token");

            var cliente = new HttpClient();

            if (tokenClaim != null)
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", tokenClaim.Value);
            }

            return cliente;
        }
    }
}