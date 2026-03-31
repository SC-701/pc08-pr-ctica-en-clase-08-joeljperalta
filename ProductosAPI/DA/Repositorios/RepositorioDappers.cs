using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA.Repositorios
{
    public class RepositorioDappers : IRepositorioDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDappers(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _conexionBaseDatos = new SqlConnection(_configuracion.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBaseDatos;
        }
    }
}
