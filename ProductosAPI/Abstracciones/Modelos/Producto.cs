using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase       
    {

        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ0-9\s\-\&]+$", ErrorMessage ="El nombre no cumple con el minimo de caracteres")]
        public string Nombre { get; set; }



        [Required(ErrorMessage = "Campo Requerido")]
        [StringLength(100, ErrorMessage ="La descripcion debe ser menor a 100 caracteres y mayor a 10", MinimumLength =10)]
        public string Descripcion { get; set; }



        [Required(ErrorMessage = "Campo Requerido")]
        public decimal Precio { get; set; }



        [Required(ErrorMessage = "Campo Requerido")]
        public int Stock { get; set; }




        [Required(ErrorMessage = "Campo Requerido")]
        public string CodigoBarras { get; set; }

    }

    public class  ProductoRequest : ProductoBase
    {
        public Guid Id { get; set; }

        public Guid IdSubcategoria { get; set; }

        public string SubCategoria { get; set; }

        public string Categoria { get; set; }


    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }

        public string Categoria { get; set; }

        public decimal PrecioUSD { get; set; }
    }
}
