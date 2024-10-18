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
          //  List<Videojuego> listaVideojuegos = new();

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

                //Agregar el registro a la lista
                listaVideojuegos.Add(videojuego);
            }

            // Cerramos la conexión
            conexion.Close();

            // Retornamos la lista de TiposEmpleado
            return listaVideojuegos;
        }

        public int Modificar(Videojuego videoJuego)
        {
            try
            {
                int i = 0;

                Conexion connec = new Conexion();
                conexion = new(connec.getCadConexion());
                conexion.Open();
                string query = "update Videojuegos " +
                    "set titulo = @titulo, genero = @genero, plataforma = @plataforma," +
                    "fechaLanzamiento = @fechaLanzamiento, desarrollador = @desarrollador, publisher = @publisher," +
                    "precio = @precio, descripcion = @descripcion where codVideojuego = @codVideojuego";

                SqlCommand comando = new SqlCommand(query,conexion);

                comando.Parameters.Add("@titulo", SqlDbType.VarChar);
                comando.Parameters.Add("@genero", SqlDbType.VarChar);
                comando.Parameters.Add("@plataforma", SqlDbType.VarChar);
                comando.Parameters.Add("@fechaLanzamiento", SqlDbType.Date);
                comando.Parameters.Add("@desarrollador", SqlDbType.VarChar);
                comando.Parameters.Add("@publisher", SqlDbType.VarChar);
                comando.Parameters.Add("@precio", SqlDbType.VarChar);
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar);
                comando.Parameters.Add("@codVideojuego", SqlDbType.VarChar);

                comando.Parameters["@titulo"].Value = videoJuego.titulo;
                comando.Parameters["@genero"].Value = videoJuego.genero;
                comando.Parameters["@plataforma"].Value = videoJuego.plataforma;
                comando.Parameters["@fechaLanzamiento"].Value = videoJuego.fechaLanzamiento;
                comando.Parameters["@desarrollador"].Value = videoJuego.desarrollador;
                comando.Parameters["@publisher"].Value = videoJuego.publisher;
                comando.Parameters["@precio"].Value = videoJuego.precio;
                comando.Parameters["@descripcion"].Value = videoJuego.descripcion;
                comando.Parameters["@codVideojuego"].Value = videoJuego.codVideojuego;

                i = comando.ExecuteNonQuery();
                conexion.Close();
                return i;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return 0;
            }
        }

        public Videojuego VideojuegoModificar(string id)
        {
            Conexion conec = new Conexion();
            conexion = new SqlConnection(conec.getCadConexion());
            conexion.Open();
            string consulta = "SELECT * FROM Videojuegos WHERE codVideojuego = @codVideojuego";

            SqlCommand comando = new SqlCommand(consulta,conexion);
            comando.Parameters.Add("@codVideojuego",SqlDbType.VarChar);

            comando.Parameters["@codVideojuego"].Value = id;

            SqlDataReader reader = comando.ExecuteReader();

            Videojuego videoJuego = new Videojuego();

            if (reader.Read())
            {
                videoJuego.codVideojuego = reader["codVideojuego"].ToString();
                videoJuego.titulo = reader["titulo"].ToString();
                videoJuego.genero = reader["genero"].ToString();
                videoJuego.plataforma = reader["plataforma"].ToString();
                videoJuego.fechaLanzamiento = Convert.ToDateTime(reader["fechaLanzamiento"]);
                videoJuego.desarrollador = reader["desarrollador"].ToString();
                videoJuego.publisher = reader["publisher"].ToString();
                videoJuego.precio = Convert.ToDecimal( reader["precio"]);
                videoJuego.descripcion = reader["descripcion"].ToString();
            }

            conexion.Close();
            return videoJuego;     
        }

    }
}
