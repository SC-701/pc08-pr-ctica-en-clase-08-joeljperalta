using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios
{
    public class TipoCambioServicios
    {
        public class BCCRResponse
        {
            public List<Dato> datos { get; set; }
        }

        public class Dato
        {
            public List<Indicador> indicadores { get; set; }
        }

        public class Indicador
        {
            public List<Serie> series { get; set; }
        }

        public class Serie
        {
            public decimal valorDatoPorPeriodo { get; set; }
        }
    }
}
