//Agregamos namespaces
using Guia7.Models;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace GamesClub.Models
{
    public class MantenimientoEmpleado
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;

        // Método para listar todos los registros de la tabla "Empleado"

        public int Ingresar(Empleado empleado) {

            try
            {   
                //crear objeto de la clase conexión
                Conexion conex = new();
                //definir la conexipin a la BD
                conexion = new(conex.getCadConexion());
                //abrir la conexión
                 conexion.Open();

                //Definir variable para almacenar el query
                SqlCommand comando = new ($"insert into Empleado (codEmpleado, IdTipoEmpleado, Nombres, Apellidos,Dui,Estado,Imagen,Usuario,Clave) values(@codEmpleado,@idTipoEmpleado,@nombres,@apellidos,@dui,@estado,@imagen,@usuario,@clave)",  conexion);
                comando.Parameters.Add("@codEmpleado", SqlDbType.VarChar);
                comando.Parameters.Add("@idTipoEmpleado", SqlDbType.VarChar);
                comando.Parameters.Add("@nombres", SqlDbType.VarChar);
                comando.Parameters.Add("@apellidos", SqlDbType.VarChar);
                comando.Parameters.Add("@dui", SqlDbType.VarChar);
                comando.Parameters.Add("@estado", SqlDbType.VarChar);
                comando.Parameters.Add("@imagen", SqlDbType.VarChar);
                comando.Parameters.Add("@usuario", SqlDbType.VarChar);
                comando.Parameters.Add("@clave", SqlDbType.VarChar);

                //Pasar los datos digitrados pó el usuario a los parametros de la instrición SQL

                comando.Parameters["@codEmpleado"].Value=empleado.codEmpleado;
                comando.Parameters["@idTipoEmpleado"].Value = empleado.IdTipoEmpleado;
                comando.Parameters["@nombres"].Value = empleado.Nombres;
                comando.Parameters["@apellidos"].Value = empleado.Apellidos;
                comando.Parameters["@dui"].Value = empleado.Dui;
                comando.Parameters["@estado"].Value = empleado.Estado;
                comando.Parameters["@imagen"].Value = empleado.Imagen;
                comando.Parameters["@usuario"].Value = empleado.Usuario;
                comando.Parameters["@clave"].Value = empleado.Clave;

                // Ejecutar instrucción SQL
                int ingresado= comando.ExecuteNonQuery();
                conexion.Close();
                // Devolvemos el numero de registros ingresados a la base
                return ingresado;


            }
            catch (Exception ex)
            {

                // Control de errores
                string error = ex.Message;
                return 0;
            }
        }




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
