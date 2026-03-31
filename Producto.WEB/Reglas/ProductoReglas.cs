using Abstracciones.Modelos;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Reglas
{
    public class ProductoReglas
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductoReglas(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private string BaseUrl()
        {
            return _configuration["ApiProductos"];
        }

        public async Task<List<ProductoResponse>> Obtener()
        {
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl() + "Producto");
                if (!response.IsSuccessStatusCode)
                    return new List<ProductoResponse>();

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                    return new List<ProductoResponse>();

                var opciones = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                };

                return JsonSerializer.Deserialize<List<ProductoResponse>>(content, opciones)
                       ?? new List<ProductoResponse>();
            }
            catch
            {
                return new List<ProductoResponse>();
            }
        }

        public async Task<ProductoResponse> Obtener(Guid id)
        {
            var response = await _httpClient.GetAsync(BaseUrl() + $"Producto/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<ProductoResponse>(content, opciones);
        }

        public async Task Agregar(ProductoRequest producto)
        {
            await _httpClient.PostAsJsonAsync(BaseUrl() + "Producto", producto);
        }

        public async Task Editar(Guid id, ProductoRequest producto)
        {
            await _httpClient.PutAsJsonAsync(BaseUrl() + $"Producto/{id}", producto);
        }

        public async Task Eliminar(Guid id)
        {
            await _httpClient.DeleteAsync(BaseUrl() + $"Producto/{id}");
        }
    }
}