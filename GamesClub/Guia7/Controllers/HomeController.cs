using Guia7.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Guia7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new();

            // Llamar al m�todo "ListarTodos" y lo pasamos como par�metro a la vista
            return View(MTipEmp.ListarTodos());
        }

        // M�todo para crear un registro en la tabla TipoEmpleado
        public IActionResult Agregar()
        {
            return View();
        }

        // Acci�n que se ejecuta al dar clic en el bot�n "Crear Nuevo"
        [HttpPost]
        public IActionResult Agregar(IFormCollection collection)
        { 
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new();

            // Crear objeto de tipo TipoEmpleado
            TipoEmpleado newTipoEmpleado = new()
            {
                IdTipoEmpleado = collection["idTipoEmpleado"],
                Descripcion = collection["descripcion"],
                Estado = Convert.ToBoolean(collection["estado"])
            };

            // Llamar al m�todo Ingresar de la clase "MantenimientoTipoEmpleado"
            MTipEmp.Ingresar(newTipoEmpleado);

            // Llamar la acci�n "Index"
            return RedirectToAction("Index");
        }
        public IActionResult Modificar(string IdTipoEmpleado)
        {
            TipoEmpleado TipoEmp = new TipoEmpleado();
            // Crear objeto de la clase MantenimientoTipoEmpleado
            MantenimientoTipoEmpleado MTipEmp = new();
            TipoEmp = MTipEmp.llenaDatos(IdTipoEmpleado);

            return View("Modificar",TipoEmp);
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
            MantenimientoTipoEmpleado MTipEmp = new();

            // Crear objeto de tipo TipoEmpleado
            TipoEmpleado newTipoEmpleado = new()
            {
                IdTipoEmpleado = collection["idTipoEmpleado"],
                Descripcion = collection["descripcion"],
                Estado = Convert.ToBoolean(collection["estado"])
            };

            // Llamar al m�todo Ingresar de la clase "MantenimientoTipoEmpleado"
            MTipEmp.Actualizar(newTipoEmpleado);

            // Llamar la acci�n "Index"
            return RedirectToAction("Index");
        }
    }
}
