using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Producto.WEB.Pages.Producto
{
    [Authorize]
    public class EliminarModel : PageModel
    {
        private readonly IConfiguration _configuracion;

        public EliminarModel(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse producto { get; set; } = default!;

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

            producto = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);

            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            string endpoint = _configuracion["ApiProductos"] + $"Producto/{id}";

            var cliente = ObtenerClienteConToken();

            var respuesta = await cliente.DeleteAsync(endpoint);

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