using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace Producto.WEB.Pages.Producto
{
    [Authorize]
    public class AgregarModel : PageModel
    {
        private readonly IConfiguration _configuracion;

        public AgregarModel(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; }

        public void OnGet()
        {
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

            string endpoint = _configuracion["ApiProductos"] + "Producto";

            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);

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