//Agregamos namespaces
using Guia7.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
namespace GamesClub.Models
{

    public class MantenimientoEmpleado
    {
        // Definir variable para establecer la conexión a base de datos
        private SqlConnection? conexion;



        // Método para listar todos los registros de la tabla "Empleado"
        public bool DuiExiste(string dui)
        {
            Conexion conex = new Conexion();
            using (SqlConnection conexion = new SqlConnection(conex.getCadConexion()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Empleado WHERE Dui = @Dui", conexion);
                comando.Parameters.AddWithValue("@Dui", dui);

                int count = (int)comando.ExecuteScalar();
                return count > 0; // Si el conteo es mayor a 0, significa que ya existe
            }
        }
        public bool CodExiste(string codEmpleado)
        {
            Conexion conex = new Conexion();
            using (SqlConnection conexion = new SqlConnection(conex.getCadConexion()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Empleado WHERE codEmpleado = @codEmpleado", conexion);
                comando.Parameters.AddWithValue("@codEmpleado", codEmpleado);

                int count = (int)comando.ExecuteScalar();
                return count > 0; // Si el conteo es mayor a 0, significa que ya existe
            }
        }
        public bool UsuarioExiste(string usuario)
        {
            Conexion conex = new Conexion();
            using (SqlConnection conexion = new SqlConnection(conex.getCadConexion()))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Empleado WHERE Usuario = @Usuario", conexion);
                comando.Parameters.AddWithValue("@Usuario", usuario);

                int count = (int)comando.ExecuteScalar();
                return count > 0; // Si el conteo es mayor a 0, significa que ya existe
            }
        }
        public Empleado Consultar(string codEmpleado) { 
            
            Conexion conex= new Conexion();
            conexion =new SqlConnection(conex.getCadConexion());

            conexion.Open();

            //Definimos una variable para indicar las senticar sql a la tabla "empleado"
            SqlCommand comando = new SqlCommand("SELECT codEmpleado,IdTipoEmpleado,Nombres,Apellidos,Dui,Estado,Imagen,Usuario,Clave FROM Empleado where codEmpleado=@codEmpleado",conexion);

            //definimos los tipo y valors de los parametros
            comando.Parameters.Add("@codEmpleado",SqlDbType.VarChar);
            comando.Parameters["@codEmpleado"].Value = codEmpleado;

            SqlDataReader Registro=comando.ExecuteReader();
            Empleado empleado = new Empleado();

            if (Registro.Read())
            {
                //asignan valores a los atributos del objeto de tipo Empleado
                empleado.codEmpleado = Registro["codEmpleado"].ToString();
                empleado.IdTipoEmpleado = Registro["IdTipoEmpleado"].ToString();
                empleado.Nombres = Registro["nombres"].ToString();
                empleado.Apellidos = Registro["apellidos"].ToString();
                empleado.Dui = Registro["dui"].ToString();
                empleado.Estado = (bool)Registro["estado"];
                empleado.Imagen = Registro["imagen"].ToString();
                empleado.Usuario = Registro["usuario"].ToString();
                empleado.Clave = Registro["clave"].ToString();


            }
            conexion.Close();
            return empleado;

        }

        public int Modificar(Empleado empleado)
        {
            try
            {
                // Verificar campos requeridos
                if (string.IsNullOrEmpty(empleado.codEmpleado) ||
                    string.IsNullOrEmpty(empleado.IdTipoEmpleado) ||
                    string.IsNullOrEmpty(empleado.Nombres) ||
                    string.IsNullOrEmpty(empleado.Apellidos) ||
                    string.IsNullOrEmpty(empleado.Dui) ||
                    empleado.Estado == null ||
                    string.IsNullOrEmpty(empleado.Usuario) ||
                    string.IsNullOrEmpty(empleado.Clave))
                {
                    return -1; // Retorna -1 si hay campos requeridos vacíos
                }

                // Crear objeto de la clase conexión
                Conexion conex = new Conexion();
                // Definir la conexión a la BD
                conexion = new SqlConnection(conex.getCadConexion());
                // Abrir la conexión
                conexion.Open();

                // Definir variable para almacenar el query de actualización
                SqlCommand comando = new SqlCommand($@"
            UPDATE Empleado 
            SET IdTipoEmpleado = @idTipoEmpleado, 
                Nombres = @nombres, 
                Apellidos = @apellidos, 
                Dui = @dui, 
                Estado = @estado, 
                Imagen = @imagen, 
                Usuario = @usuario, 
                Clave = @clave 
            WHERE codEmpleado = @codEmpleado", conexion);

                // Agregar los parámetros
                comando.Parameters.Add("@codEmpleado", SqlDbType.VarChar).Value = empleado.codEmpleado;
                comando.Parameters.Add("@idTipoEmpleado", SqlDbType.VarChar).Value = empleado.IdTipoEmpleado;
                comando.Parameters.Add("@nombres", SqlDbType.VarChar).Value = empleado.Nombres;
                comando.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = empleado.Apellidos;
                comando.Parameters.Add("@dui", SqlDbType.VarChar).Value = empleado.Dui;
                comando.Parameters.Add("@estado", SqlDbType.Bit).Value = empleado.Estado;
                comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = empleado.Usuario;
                comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = empleado.Clave;

                // Manejar opcionalmente el campo "Imagen"
                comando.Parameters.Add("@imagen", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(empleado.Imagen))
                {
                    comando.Parameters["@imagen"].Value = empleado.Imagen; // Si tiene valor, lo establece
                }
                else
                {
                    comando.Parameters["@imagen"].Value = DBNull.Value; // Si es nulo o vacío, se pasa DBNull
                }

                // Ejecutar instrucción SQL
                int actualizado = comando.ExecuteNonQuery();
                conexion.Close();

                // Devolver el número de registros actualizados
                return actualizado;
            }
            catch (Exception ex)
            {
                // Control de errores
                string error = ex.Message;
                return 0;
            }
        }
        //a


        public int Ingresar(Empleado empleado) {
          
            
            
           
            try
            {
                // Verificar campos requeridos
                if (string.IsNullOrEmpty(empleado.codEmpleado) ||
                    string.IsNullOrEmpty(empleado.IdTipoEmpleado) ||
                    string.IsNullOrEmpty(empleado.Nombres) ||
                    string.IsNullOrEmpty(empleado.Apellidos) ||
                    string.IsNullOrEmpty(empleado.Dui) ||
                    empleado.Estado == null ||
                    string.IsNullOrEmpty(empleado.Imagen) ||
                    string.IsNullOrEmpty(empleado.Usuario) ||
                    string.IsNullOrEmpty(empleado.Clave))
                {
                    return -1; // Retorna -1 si hay campos requeridos vacíos
                }
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
                comando.Parameters.Add("@estado", SqlDbType.Bit);
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
        public List<TipoEmpleado> listaTEmpleado()
        {
            Conexion conex = new Conexion();
            List<TipoEmpleado> listaTipoEmpleado = new List<TipoEmpleado>();

            // Definir la conexión a la BD
            using (SqlConnection conexion = new SqlConnection(conex.getCadConexion()))
            {
                // Abrir la conexión 
                conexion.Open();

                // Crear el comando SQL
                SqlCommand comando = new SqlCommand("SELECT IdTipoEmpleado, Descripcion FROM TipoEmpleado", conexion);

                // Ejecutar el comando y leer los datos
                SqlDataReader leer = comando.ExecuteReader();
                while (leer.Read())
                {
                    // Crear el objeto TipoEmpleado
                    TipoEmpleado tipoEmpleado = new TipoEmpleado()
                    {
                        IdTipoEmpleado = leer["IdTipoEmpleado"].ToString(),   // Verifica que la columna se llame exactamente así en la base de datos
                        Descripcion = leer["Descripcion"].ToString()          // Asegúrate que la columna se llama Descripcion
                    };

                    // Agregar el objeto a la lista
                    listaTipoEmpleado.Add(tipoEmpleado);
                }
            }

            // Retornar la lista de TipoEmpleado
            return listaTipoEmpleado;
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
