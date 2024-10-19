using Guia7.Models;
using System.Data.SqlClient;
using System.Data;

namespace GamesClub.Models
{
    public class MantenimientoVideojuego
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;

        // Definir una lista de tipo "Videojuegos"
        List<Videojuego> listaVideojuegos = new();

        // Método para agregar un Videojuego
        public int Ingresar(Videojuego videojuego)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"insert into Videojuegos (codVideojuego, titulo, genero, plataforma, fechaLanzamiento, desarrollador, publisher, precio, descripcion) " +
                    $"values (@codVideojuego, @titulo, @genero, @plataforma, @fechaLanzamiento, @desarrollador, @publisher, @precio, @descripcion)", conexion);
                comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar);
                comando.Parameters.Add("@titulo", SqlDbType.VarChar);
                comando.Parameters.Add("@genero", SqlDbType.VarChar);
                comando.Parameters.Add("@plataforma", SqlDbType.VarChar);
                comando.Parameters.Add("@fechaLanzamiento", SqlDbType.Date);
                comando.Parameters.Add("@desarrollador", SqlDbType.VarChar);
                comando.Parameters.Add("@publisher", SqlDbType.VarChar);
                comando.Parameters.Add("@precio", SqlDbType.Decimal);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar);

                // Pasar los datos digitados por el usuario a los parámetros
                comando.Parameters["@codVideojuego"].Value = videojuego.codVideojuego;
                comando.Parameters["@titulo"].Value = videojuego.titulo;
                comando.Parameters["@genero"].Value = videojuego.genero;
                comando.Parameters["@plataforma"].Value = videojuego.plataforma;
                comando.Parameters["@fechaLanzamiento"].Value = videojuego.fechaLanzamiento;
                comando.Parameters["@desarrollador"].Value = videojuego.desarrollador;
                comando.Parameters["@publisher"].Value = videojuego.publisher;
                comando.Parameters["@precio"].Value = videojuego.precio;
                comando.Parameters["@descripcion"].Value = videojuego.descripcion;

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

        // Método para listar todos los registros de la tabla "Videojuegos"
        public List<Videojuego> ListarTodos()
        {
            // Crear objeto de la clase conexión
            Conexion conn = new();

            // Definir una lista de tipo "Videojuegos"
            // List<Videojuego> listaVideojuegos = new();

            // Definir la conexión a la BD
            conexion = new(conn.getCadConexion());
            conexion.Open();

            // Definir variable para almacenar el query
            SqlCommand comando = new($"select * from Videojuegos", conexion);

            // Crear un objeto SqlDataReader
            SqlDataReader lector = comando.ExecuteReader();

            // Para cada registro
            while (lector.Read())
            {
                // Crear un objeto de tipo Videojuego
                Videojuego videojuego = new()
                {
                    // Almacenar los datos de cada registro en los atributos del nuevo objeto
                    codVideojuego = lector["codVideojuego"].ToString(),
                    titulo = lector["titulo"].ToString(),
                    genero = lector["genero"].ToString(),
                    plataforma = lector["plataforma"].ToString(),
                    fechaLanzamiento = (DateTime)lector["fechaLanzamiento"],
                    desarrollador = lector["desarrollador"].ToString(),
                    publisher = lector["publisher"].ToString(),
                    precio = (decimal)lector["precio"],
                    descripcion = lector["descripcion"].ToString()
                };

                // Agregar cada videojuego a la lista de videojuegos
                listaVideojuegos.Add(videojuego);
            }
            conexion.Close();
            return listaVideojuegos;
        }

        // Método para modificar un Videojuego
        public int Modificar(Videojuego videojuego)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"update Videojuegos set titulo = @titulo, genero = @genero, plataforma = @plataforma, fechaLanzamiento = @fechaLanzamiento, " +
                    $"desarrollador = @desarrollador, publisher = @publisher, precio = @precio, descripcion = @descripcion where codVideojuego = @codVideojuego", conexion);
                comando.Parameters.Add("@titulo", SqlDbType.VarChar);
                comando.Parameters.Add("@genero", SqlDbType.VarChar);
                comando.Parameters.Add("@plataforma", SqlDbType.VarChar);
                comando.Parameters.Add("@fechaLanzamiento", SqlDbType.Date);
                comando.Parameters.Add("@desarrollador", SqlDbType.VarChar);
                comando.Parameters.Add("@publisher", SqlDbType.VarChar);
                comando.Parameters.Add("@precio", SqlDbType.Decimal);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
                comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar);

                // Pasar los datos digitados por el usuario a los parámetros
                comando.Parameters["@titulo"].Value = videojuego.titulo;
                comando.Parameters["@genero"].Value = videojuego.genero;
                comando.Parameters["@plataforma"].Value = videojuego.plataforma;
                comando.Parameters["@fechaLanzamiento"].Value = videojuego.fechaLanzamiento;
                comando.Parameters["@desarrollador"].Value = videojuego.desarrollador;
                comando.Parameters["@publisher"].Value = videojuego.publisher;
                comando.Parameters["@precio"].Value = videojuego.precio;
                comando.Parameters["@descripcion"].Value = videojuego.descripcion;
                comando.Parameters["@codVideojuego"].Value = videojuego.codVideojuego;

                // Ejecutar instrucción SQL
                int modificados = comando.ExecuteNonQuery();
                conexion.Close();

                // Devolvemos el numero de registros modificados en la base
                return modificados;
            }
            catch (Exception ex)
            {
                // Control de errores
                string error = ex.Message;
                return 0;
            }
        }

        // Método para verificar si el código de videojuego ya existe
        public bool CodExiste(string codVideojuego)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"select * from Videojuegos where codVideojuego = @codVideojuego", conexion);
                comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar).Value = codVideojuego;

                // Crear un objeto SqlDataReader
                SqlDataReader lector = comando.ExecuteReader();

                // Verificar si el código ya existe
                return lector.HasRows;
            }
            catch (Exception ex)
            {
                // Control de errores
                string error = ex.Message;
                return false;
            }
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        // Método para eliminar un Videojuego
        public int Eliminar(string codVideojuego)
        {
            try
            {
                // Crear objeto de la clase conexión
                Conexion conn = new();

                // Definir la conexión a la BD
                conexion = new(conn.getCadConexion());
                conexion.Open();

                // Definir variable para almacenar el query
                SqlCommand comando = new($"delete from Videojuegos where codVideojuego = @codVideojuego", conexion);
                comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar);
                comando.Parameters["@codVideojuego"].Value = codVideojuego;

                // Ejecutar instrucción SQL
                int eliminados = comando.ExecuteNonQuery();
                conexion.Close();

                // Devolvemos el numero de registros eliminados
                return eliminados;
            }
            catch (Exception ex)
            {
                // Control de errores
                string error = ex.Message;
                return 0;
            }
        }

        // Método para obtener un videojuego para modificar
        public Videojuego VideojuegoModificar(string IdTipoEmpleado)
        {
            // Crear objeto de la clase conexión
            Conexion conn = new();

            // Definir la conexión a la BD
            conexion = new(conn.getCadConexion());
            conexion.Open();

            // Definir variable para almacenar el query
            SqlCommand comando = new($"select * from Videojuegos where codVideojuego = @codVideojuego", conexion);
            comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar);
            comando.Parameters["@codVideojuego"].Value = IdTipoEmpleado;

            // Crear un objeto SqlDataReader
            SqlDataReader lector = comando.ExecuteReader();

            Videojuego videojuego = new();

            // Obtener el videojuego
            if (lector.Read())
            {
                videojuego.codVideojuego = lector["codVideojuego"].ToString();
                videojuego.titulo = lector["titulo"].ToString();
                videojuego.genero = lector["genero"].ToString();
                videojuego.plataforma = lector["plataforma"].ToString();
                videojuego.fechaLanzamiento = (DateTime)lector["fechaLanzamiento"];
                videojuego.desarrollador = lector["desarrollador"].ToString();
                videojuego.publisher = lector["publisher"].ToString();
                videojuego.precio = (decimal)lector["precio"];
                videojuego.descripcion = lector["descripcion"].ToString();
            }
            conexion.Close();
            return videojuego;
        }
    }
}

