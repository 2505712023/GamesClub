using GamesClub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GamesClub.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Empleado()
        {
           MantenimientoEmpleado mEmpleado = new MantenimientoEmpleado();

            return View(mEmpleado.ListarTodos());
        }
    }
}
