using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid Id { get; set; }


        public Guid IdCategoria { get; set; }


        public Guid IdSubcategoria { get; set; }


        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdSubcategoria { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioUSD { get; set; }
    }
}