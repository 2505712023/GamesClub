using GamesClub.Models;
using Guia7.Controllers;
using Guia7.Models;
using Microsoft.AspNetCore.Mvc;

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
                precio = Convert.ToDecimal(collection["precio"].ToString().Replace("$","")),
                descripcion = collection["descripcion"].ToString()
            };

            // Llamar al método Ingresar de la clase "MantenimientoVideojuego"
            MVideojuego.Ingresar(videojuego);

            // Llamar la acción "Index"
            return RedirectToAction("Index");
        }
    }
}
