using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private IProductoDA _productoDA;
        private readonly IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA producto, IProductoReglas productoReglas)
        {
            _productoDA = producto;
            _productoReglas = productoReglas;
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            var productos = (await _productoDA.Obtener()).ToList();

            await Task.WhenAll(productos.Select(async p =>
                p.PrecioUSD = await CalcularPrecioUSD(p.Precio)
            ));

            return productos;
        }


        /*

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);

            producto.PrecioUSD = await CalcularPrecioUSD(producto.Precio);

            return producto;
        }
      */

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);

            if (producto == null)
                return null;

            producto.PrecioUSD = await CalcularPrecioUSD(producto.Precio);

            return producto;
        }


        public async Task<decimal> CalcularPrecioUSD(decimal precio)
        {
            return await _productoReglas.CalcularPrecioUSD(precio);
        }

    }
}
