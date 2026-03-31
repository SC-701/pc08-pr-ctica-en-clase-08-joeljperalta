using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TipoCambioServicio(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambio()
        {
            var urlBase = _configuration["BancoCentralCR:UrlBase"];
            var token = _configuration["BancoCentralCR:BearerToken"];

            var hoy = DateTime.Now.ToString("yyyy/MM/dd");
            var url = $"{urlBase}?fechaInicio={hoy}&fechaFin={hoy}&idioma=ES";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<TipoCambioResponse>(json);

            return data.datos[0].indicadores[0].series[0].valorDatoPorPeriodo;
        }
    }
}