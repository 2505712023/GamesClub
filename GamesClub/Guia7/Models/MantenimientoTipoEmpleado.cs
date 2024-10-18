using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace Guia7.Models
{
    public class MantenimientoTipoEmpleado
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;
        DataTable Registro = new DataTable();
        // Método para agregar un tipo de empleados
        public int Ingresar(TipoEmpleado tipoEmpleado)
        {
            try
            {
                //validacion para que no se repita el tipo de comprobante

                if (ValidaTipoEmpleado(tipoEmpleado.IdTipoEmpleado).Rows.Count > 0)
                {
                    return 0;
                }


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
                    Estado = (bool)lector["Estado"],
                };

                //Agregar el registro a la lista
                listaTiposEmpleado.Add(newTipoEmpleado);
            }

            // Cerramos la conexión
            conexion.Close();

            // Retornamos la lista de TiposEmpleado
            return listaTiposEmpleado;
        }
        public DataTable ValidaTipoEmpleado(string IdTipoEmpleado)
        {
            string msj = "";

            // Crear objeto de la clase conexión
            Conexion conn = new();

            // Definir una lista de tipo "TipoEmpleado"
            List<TipoEmpleado> listaTiposEmpleado = new();

            // Definir la conexión a la BD
            conexion = new(conn.getCadConexion());
            conexion.Open();

            // Definir variable para almacenar el query
            SqlCommand comando = new($"select * from TipoEmpleado where IdTipoEmpleado = @IdTipoEmpleado", conexion);
            comando.Parameters.Add("@IdTipoEmpleado", SqlDbType.VarChar);
            comando.Parameters["@IdTipoEmpleado"].Value = IdTipoEmpleado;
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                Registro.Load(reader);
            }

            return Registro;
        }
        public TipoEmpleado ObtenerTipoEmpleado(string id)
        {
            // Crear objeto de la clase conexión
            Conexion conn = new Conexion();
            // Definir la conexión a la BD
            conexion = new SqlConnection(conn.getCadConexion());
            conexion.Open();


            SqlCommand comando = new SqlCommand("SELECT * FROM TipoEmpleado WHERE IdTipoEmpleado = @IdTipoEmpleado",conexion);

            comando.Parameters.Add("@IdTipoEmpleado", SqlDbType.VarChar);
            comando.Parameters["@IdTipoEmpleado"].Value = id;

            SqlDataReader Registros = comando.ExecuteReader();

            TipoEmpleado tipoEmp = new TipoEmpleado();

            if (Registros.Read())
            {
                tipoEmp.IdTipoEmpleado = Registros["IdTipoEmpleado"].ToString();
                tipoEmp.Descripcion = Registros["Descripcion"].ToString();
                tipoEmp.Estado = (bool)Registros["Estado"];
            }

            conexion.Close();

            return tipoEmp;
        }

        public TipoEmpleado llenaDatos(string IdTipoEmpleado)
        {
            // Crear objeto de la clase conexión
            Conexion conn = new();
            // Definir la conexión a la BD
            conexion = new(conn.getCadConexion());
            conexion.Open();
            TipoEmpleado DaTipoEmpleado = new TipoEmpleado();
            SqlCommand comando = new SqlCommand("SELECT * FROM TipoEmpleado WHERE IdTipoEmpleado = @IdTipoEmpleado", conexion);
            comando.Parameters.Add("@IdTipoEmpleado", SqlDbType.VarChar).Value = IdTipoEmpleado;

            // Crear el DataTable
            DataTable registro = new DataTable();

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                // Cargar el DataTable con los datos del SqlDataReader
                registro.Load(reader);
            }

            DaTipoEmpleado.IdTipoEmpleado = Convert.ToString(registro.Rows[0]["IdTipoEmpleado"]);
            DaTipoEmpleado.Descripcion = Convert.ToString(registro.Rows[0]["Descripcion"]);
            DaTipoEmpleado.Estado = Convert.ToBoolean(registro.Rows[0]["Estado"]);

            return DaTipoEmpleado;
        }
        public int Modificar(TipoEmpleado tipoEmpleado)
        {
            try
            {
                //validacion para que no se repita el tipo de comprobante

                if (ValidaTipoEmpleado(tipoEmpleado.IdTipoEmpleado).Rows.Count > 1)
                {
                    return 0;
                }


                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"update TipoEmpleado set Descripcion = @descripcion, Estado = @estado where idTipoEmpleado = @idTipoEmpleado ", conexion);
                comando.Parameters.Add("@idTipoEmpleado", SqlDbType.VarChar);
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
        public int Borrar(string IdTipoEmpleado)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new("delete from TipoEmpleado where IdTipoEmpleado = @idTipoEmpleado", conexion); //consulta de delete
                comando.Parameters.Add("@idTipoEmpleado", SqlDbType.VarChar);
                comando.Parameters["@idTipoEmpleado"].Value = IdTipoEmpleado;

                // Ejecutar instrucción SQL
                int i = comando.ExecuteNonQuery();
                conexion.Close();
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }
}
