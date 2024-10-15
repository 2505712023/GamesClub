using GamesClub.Models;
using Guia7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        /*Acció  que tiene por objeto mostrar la vista con un formulario HTML que permita ingresar los datos deun tipo de empleado*/


        public IActionResult Agregar()
        {
            // Crear instancia de MantenimientoEmpleado para acceder a los datos
            MantenimientoEmpleado mEmpleado = new MantenimientoEmpleado();

            // Cargar la lista de TipoEmpleado y pasarlo a la vista
            ViewBag.TipoEmpleados = mEmpleado.listaTEmpleado();
            return View();

        }

        // Acción que se ejecutará cuando se presione el botón de tipo submit, recibe como parámetros los datos cargador en el formulario
        [HttpPost]
        public IActionResult Agregar(IFormCollection collection)
        {

            MantenimientoEmpleado mEmpleado = new();
            ViewBag.TipoEmpleados = mEmpleado.listaTEmpleado();


            //Verificar si no hay errores
            if (ModelState.IsValid)
            {
                //Definimos un objeto de tipo mantenimiento empleado+
               
              
                if (mEmpleado.CodExiste(collection["codEmpleado"]))
                {
                    // Si el Codigo existe, envía un mensaje de error a la vista
                    TempData["ErrorMessage"] = "El Codigo de Empleado ya existe";
                    return View();
                }
                if (mEmpleado.DuiExiste(collection["dui"]))
                {
                    // Si el Dui existe, envía un mensaje de error a la vista
                    TempData["ErrorMessage"] = "El Dui ya existe";
                    return View();
                }
                if (mEmpleado.UsuarioExiste(collection["usuario"]))
                {
                    // Si el Codigo existe, envía un mensaje de error a la vista
                    TempData["ErrorMessage"] = "El usuario de Empleado ya existe";
                    return View();
                }

              Empleado emp = new()
        {
            codEmpleado = collection["codEmpleado"].ToString(),
            IdTipoEmpleado = collection["idTipoEmpleado"].ToString(),
            Nombres = collection["nombres"].ToString(),
            Apellidos = collection["apellidos"].ToString(),
            Dui = collection["dui"].ToString(),
            // Verificamos si el valor de estado es nulo o vacío
            Estado = !string.IsNullOrEmpty(collection["estado"]) && bool.Parse(collection["estado"]),
            Imagen = collection["imagen"].ToString(),
            Usuario = collection["usuario"].ToString(),
            Clave = collection["clave"].ToString()
        };

        // Verificar campos requeridos
        if (string.IsNullOrEmpty(emp.codEmpleado) ||
            string.IsNullOrEmpty(emp.IdTipoEmpleado) ||
            string.IsNullOrEmpty(emp.Nombres) ||
            string.IsNullOrEmpty(emp.Apellidos) ||
            string.IsNullOrEmpty(emp.Dui) ||
            string.IsNullOrEmpty(emp.Imagen) ||
            string.IsNullOrEmpty(emp.Usuario) ||
            string.IsNullOrEmpty(emp.Clave))
        {
            TempData["ErrorMessage"] = "Todos los campos son requeridos.";
            return View(); // Regresar a la vista con el mensaje de error
        }
        //listo
        // Llamamos el método "Ingresar"
        int resultado = mEmpleado.Ingresar(emp);

        // Verificamos el resultado de la inserción
        if (resultado <= 0) // Cambié a <= para incluir errores o 0 registros
        {
            TempData["ErrorMessage"] = "Hubo un error al guardar los datos.";
            return View(); // Regresar a la vista con el mensaje de error
        }
                //Invocamos la Acción Empleado

                return RedirectToAction("Empleado");
            }
            else
            {
                // En caso de error, mostrar mensaje o volver a la vista de agregar
                ViewBag.ErrorMessage = "Error al guardar los datos.";
                return View();
            }

        }
    }
    
}