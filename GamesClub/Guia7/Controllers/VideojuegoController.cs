using GamesClub.Models;
using Guia7.Controllers;
using Guia7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GamesClub.Controllers
{
    public class VideojuegoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public VideojuegoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Crear objeto de la clase MantenimientoVideojuego
            MantenimientoVideojuego MVideojuego = new();

            // Llamar al método "ListarTodos" y lo pasamos como parámetro a la vista
            return View(MVideojuego.ListarTodos());
        }

        // Método para crear un registro en la tabla Videojuegos
        public IActionResult Agregar()
        {
            return View();
        }

        // Acción que se ejecuta al dar clic en el botón "Crear Nuevo"
        [HttpPost]
        public IActionResult Agregar(IFormCollection collection)
        {
            // Crear objeto de la clase MantenimientoVideojuego
            MantenimientoVideojuego MVideojuego = new();

            // Crear objeto de tipo Videojuego
            Videojuego videojuego = new()
            {
                codVideojuego = collection["codVideojuego"].ToString(),
                titulo = collection["titulo"].ToString(),
                genero = collection["genero"].ToString(),
                plataforma = collection["plataforma"].ToString(),
                fechaLanzamiento = Convert.ToDateTime(collection["fechaLanzamiento"]),
                desarrollador = collection["desarrollador"].ToString(),
                publisher = collection["publisher"].ToString(),
                precio = Convert.ToDecimal(collection["precio"].ToString().Replace("$", "")),
                descripcion = collection["descripcion"].ToString()
            };

            // Llamar al método Ingresar de la clase "MantenimientoVideojuego"
            MVideojuego.Ingresar(videojuego);

            // Llamar la acción "Index"
            return RedirectToAction("Index");
        }

        // Acción GET para mostrar la página de confirmación de eliminación
        public IActionResult Eliminar(String codVideojuego)
        {
            //Definimos un objeto de tipo "MantenimientoTipoEmpleado"
            MantenimientoVideojuego MVideojuego = new();

            //Llamamos al método "Borrar"
            MVideojuego.Eliminar(codVideojuego);

            //Invocamos acción "Index"

            return RedirectToAction("Index");
        }

        //// Acción POST para confirmar y realizar la eliminación
        //[HttpPost, ActionName("Eliminar")]
        //[ValidateAntiForgeryToken]
        //public IActionResult ConfirmarEliminacion(int id)
        //{
        //    // Crear objeto de la clase MantenimientoVideojuego
        //    MantenimientoVideojuego MVideojuego = new();

        //    // Obtener el videojuego por su id
        //    Videojuego videojuego = MVideojuego.ObtenerPorId(id);

        //    if (videojuego == null)
        //    {
        //        return NotFound(); // Si no se encuentra, retornar 404
        //    }

        //    // Llamar al método para eliminar el videojuego
        //    MVideojuego.Eliminar(id);

        //    // Redirigir a la página principal después de eliminar
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
