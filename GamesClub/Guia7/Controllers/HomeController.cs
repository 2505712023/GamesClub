using GamesClub.Models;
using Guia7.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks.Sources;

namespace Guia7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Usar TempData en Razor Pages
        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // Código de lógica para OnGet
        }
        public IActionResult Index()
        {
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new();

            // Llamar al método "ListarTodos" y lo pasamos como parámetro a la vista
            return View(MTipEmp.ListarTodos());
        }

        // Método para crear un registro en la tabla TipoEmpleado
        public IActionResult Agregar()
        {
            return View();
        }

        // Acción que se ejecuta al dar clic en el botón "Crear Nuevo"
        [HttpPost]
        public IActionResult Agregar(IFormCollection collection)
        {
            MantenimientoTipoEmpleado mTEmpleado = new();
            if (mTEmpleado.IdExiste(collection["idTipoEmpleado"]))
            {
                TempData["ErrorMessage"] = "El Código de Tipo Empleado ya existe";
                return View(); // Regresa a la misma vista
            }


            //verifica si hay errores
            if (!ModelState.IsValid)
            {
                // Verifica si el idTipoEmpleado ya existe
               

                return View(); // Si hay otros errores de validación
            }
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new();

            bool estado = collection["estado"].Count > 1 ?
                  collection["estado"][0] == "true" :
                  Convert.ToBoolean(collection["estado"]);

            // Crear objeto de tipo TipoEmpleado
            TipoEmpleado newTipoEmpleado = new()
            {
                IdTipoEmpleado = collection["idTipoEmpleado"],
                Descripcion = collection["descripcion"],
                Estado = Convert.ToBoolean(collection["estado"][0])
            };

            // Llamar al método Ingresar de la clase "MantenimientoTipoEmpleado"
            MTipEmp.Ingresar(newTipoEmpleado);

            // Llamar la acción "Index"
            return RedirectToAction("Index");
        }
        public IActionResult Modificar(string IdTipoEmpleado)
        {
      
            try
            {
                MantenimientoTipoEmpleado MtipoEmp = new MantenimientoTipoEmpleado();
                // Crear objeto de la clase MantenimientoTipoEmpleado
                TipoEmpleado TipEmp = MtipoEmp.ObtenerTipoEmpleado(IdTipoEmpleado);

                return View(TipEmp);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Ocurrió un error al eliminar: {ex.Message}";
                return View("Index");  // Volver a la misma página
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Modificar(IFormCollection collection)
        {
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new MantenimientoTipoEmpleado();

            // Crear objeto de tipo TipoEmpleado
            TipoEmpleado tipoEmp = new TipoEmpleado()
            {
                IdTipoEmpleado = collection["idTipoEmpleado"],
                Descripcion = collection["descripcion"],
                Estado = bool.Parse(collection["estado"])
            };

            // Llamar al método Ingresar de la clase "MantenimientoTipoEmpleado"
            MTipEmp.Modificar(tipoEmp);

            // Llamar la acción "Index"
            return RedirectToAction("Index");
        }
        //Acción que tiene por objetivo eliminar los datos de un alumno
        public IActionResult Eliminar(String IdTipoEmpleado)
        {
            MantenimientoTipoEmpleado MTipoEmp = new MantenimientoTipoEmpleado();
            string errorMessage;
            int resultado = MTipoEmp.Borrar(IdTipoEmpleado, out errorMessage);

            if (resultado == 0 && !string.IsNullOrEmpty(errorMessage))
            {
                // Si hubo un error, pasar el mensaje de error a la vista
                TempData["ErrorMessage"] = errorMessage;
            }

            return RedirectToAction("Index");
        }

    }
}
