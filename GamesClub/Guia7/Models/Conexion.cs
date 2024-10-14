using System.Text;

namespace Guia7.Models
{
    public class Conexion
    {
        private string? cadConexion = string.Empty;

        public Conexion()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // Definir la cadena de conexión
            cadConexion = builder.GetSection("ConnectionStrings:CadenaConexion").Value;
        }

        // Método para leer cadena de conexión
        public string? getCadConexion()
        {
            return cadConexion;
        }
    }
}
