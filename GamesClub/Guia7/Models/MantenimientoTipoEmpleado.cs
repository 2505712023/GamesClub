using System.Data;
using System.Data.SqlClient;

namespace Guia7.Models
{
    public class MantenimientoTipoEmpleado
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;

        // Método para agregar un tipo de empleados

        public int Ingresar (TipoEmpleado tipoEmpleado)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"insert into TipoEmpleado (idTipoEmpleado, Descripcion, Estado) values (@idTipoEmpleado, @descripcion, @estado)", conexion);
                comando.Parameters.Add("@idTIpoEmpleado", SqlDbType.VarChar);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
                comando.Parameters.Add("@estado", SqlDbType.Bit);

                // Pasar los datos digitados por el usuario a los parámetros
                comando.Parameters["@idTipoEmpleado"].Value = tipoEmpleado.IdTipoEmpleado;
                comando.Parameters["@descripcion"].Value = tipoEmpleado.Descripcion;
                comando.Parameters["@estado"].Value = tipoEmpleado.Estado;

                // Ejecutar instrucción SQL
                int ingresado = comando.ExecuteNonQuery();
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

        // Método para listar todos los registros de la tabla "TipoEmpleado"
        public List<TipoEmpleado> ListarTodos()
        {
            // Crear objeto de la clase conexión
            Conexion conn = new();

            // Definir una lista de tipo "TipoEmpleado"
            List<TipoEmpleado> listaTiposEmpleado = new();

            // Definir la conexión a la BD
            conexion = new(conn.getCadConexion());
            conexion.Open();

            // Definir variable para almacenar el query
            SqlCommand comando = new($"select * from TipoEmpleado", conexion);

            // Crear un objeto SqlDataReader
            SqlDataReader lector = comando.ExecuteReader();

            // Para cada registro
            while (lector.Read())
            {
                // Crear un objeto de tipo TipoEmpleado
                TipoEmpleado newTipoEmpleado = new()
                {
                    // Almacenar los datos de cada registro en los atributos del nuevo objeto
                    IdTipoEmpleado = lector["IdTipoEmpleado"].ToString(),
                    Descripcion = lector["Descripcion"].ToString(),
                    Estado = (bool) lector["Estado"],
                };

                //Agregar el registro a la lista
                listaTiposEmpleado.Add(newTipoEmpleado);
            }

            // Cerramos la conexión
            conexion.Close();

            // Retornamos la lista de TiposEmpleado
            return listaTiposEmpleado;
        }
    }
}
