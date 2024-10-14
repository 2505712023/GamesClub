//Agregamos namespaces
using Guia7.Models;
using System.Data;
using System.Data.SqlClient;

namespace GamesClub.Models
{
    public class MantenimientoEmpleado
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;

        // Método para listar todos los registros de la tabla "Empleado"


        public List<Empleado> ListarTodos() {
        //Crear objeto de tipo "Conexion" para iniciar la conexión a la base de datos "GamesClub"
        Conexion conex =new ();

            //Definir la conexión a la BD
        List<Empleado> Empleado = new List<Empleado>();

            // Definir la conexión a la BD
            conexion = new(conex.getCadConexion());
            //Abrir la conexión 
            conexion.Open();

            //Definimos una variable para indicar las sentencia SQL  a la tabla "Empleado"
            SqlCommand comando = new SqlCommand($"select codEmpleado, temp.Descripcion, Nombres,Apellidos, Dui, e.Estado,Imagen,Usuario,Clave from Empleado e inner join TipoEmpleado temp on e.IdTipoEmpleado= temp.IdTipoEmpleado", conexion);
        //Dwfinir un objeto "DataReader" que almacenará los registros de la tabla "Empleado"
            SqlDataReader leer = comando.ExecuteReader();

            while (leer.Read()) {

                //Creamos un objeto de tipo "Empleado"
                Empleado empleado = new Empleado()
                {
                    //Almacenamos los datos de cada registros en los atributos de la clase
                    codEmpleado = leer["codEmpleado"].ToString(),
                    IdTipoEmpleado = leer["Descripcion"].ToString(),
                    Nombres = leer["nombres"].ToString(),
                    Apellidos= leer["apellidos"].ToString(),
                    Dui = leer["dui"].ToString(),
                    Estado = (bool)leer["estado"],
                    Imagen= leer["imagen"].ToString(),
                    Usuario= leer["usuario"].ToString(),
                    Clave= leer["clave"].ToString()


                };
                //Agregamos el registro a la lista
                Empleado.Add(empleado); 
            }
            //Cerrar conexión
           conexion.Close();

            return Empleado;
        }

    }
}
