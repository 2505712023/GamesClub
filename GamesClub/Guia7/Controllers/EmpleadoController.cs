﻿using GamesClub.Models;
using Guia7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Modificar(string codEmpleado)
        {
            MantenimientoEmpleado mEmpleado = new MantenimientoEmpleado();

            // Consultar los datos del empleado
            Empleado empleado = mEmpleado.Consultar(codEmpleado);

            // Obtener lista de tipos de empleados
            List<TipoEmpleado> tiposEmpleado = mEmpleado.listaTEmpleado();

            // Crear ViewBag para pasar la lista de tipos de empleado
            ViewBag.TiposEmpleado = tiposEmpleado.Select(te => new { Value = te.IdTipoEmpleado, Text = te.Descripcion }).ToList();

            return View(empleado);
        }

        [HttpPost]

        [HttpPost]
        public IActionResult Modificar(IFormCollection collection)
        {
            MantenimientoEmpleado mEmpleado = new();
            ViewBag.TipoEmpleados = mEmpleado.listaTEmpleado();

            if (ModelState.IsValid)
            {
               


                Empleado emp = new Empleado
                {
                    codEmpleado = collection["codEmpleado"].ToString(),
                    IdTipoEmpleado = collection["idTipoEmpleado"].ToString(),
                    Nombres = collection["nombres"].ToString(),
                    Apellidos = collection["apellidos"].ToString(),
                    Dui = collection["dui"].ToString(),
                    Estado = !string.IsNullOrEmpty(collection["estado"]) && bool.Parse(collection["estado"]),
                    Usuario = collection["usuario"].ToString(),
                    Clave = collection["clave"].ToString(),
                };

                // Manejar la carga de la imagen
                var imagenFile = collection.Files["imagen"]; // Asegúrate de que el nombre del campo sea correcto
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagenFile.CopyTo(memoryStream);
                        emp.Imagen = Convert.ToBase64String(memoryStream.ToArray()); // Almacena la imagen en Base64
                    }
                }
                else
                {
                    // Si no se proporciona una nueva imagen, puedes mantener la imagen existente
                    var empleadoExistente = mEmpleado.Consultar(emp.codEmpleado);
                    emp.Imagen = empleadoExistente.Imagen; // Asigna la imagen existente
                }

                // Verificar campos requeridos
                if (string.IsNullOrEmpty(emp.codEmpleado) ||
                    string.IsNullOrEmpty(emp.IdTipoEmpleado) ||
                    string.IsNullOrEmpty(emp.Nombres) ||
                    string.IsNullOrEmpty(emp.Apellidos) ||
                    string.IsNullOrEmpty(emp.Dui) ||
                    string.IsNullOrEmpty(emp.Imagen) || // Aquí se asegura que 'emp.Imagen' no es null
                    string.IsNullOrEmpty(emp.Usuario) ||
                    string.IsNullOrEmpty(emp.Clave))
                {
                    TempData["ErrorMessage"] = "Todos los campos son requeridos.";
                    return View(); // Regresar a la vista con el mensaje de error
                }

                int resultado = mEmpleado.Modificar(emp);

                if (resultado <= 0)
                {
                    TempData["ErrorMessage"] = "Hubo un error al guardar los datos.";
                    return View(); // Regresar a la vista con el mensaje de error
                }
                return RedirectToAction("Empleado");
            }
            else
            {
                // En caso de error, mostrar mensaje o volver a la vista de modificar
                ViewBag.ErrorMessage = "Error al guardar los datos.";
                return View();
            }
        }


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

            if (ModelState.IsValid)
            {
                if (mEmpleado.CodExiste(collection["codEmpleado"]))
                {
                    TempData["ErrorMessage"] = "El Código de Empleado ya existe";
                    return View();
                }
                if (mEmpleado.DuiExiste(collection["dui"]))
                {
                    TempData["ErrorMessage"] = "El DUI ya existe";
                    return View();
                }
                if (mEmpleado.UsuarioExiste(collection["usuario"]))
                {
                    TempData["ErrorMessage"] = "El usuario de Empleado ya existe";
                    return View();
                }

                Empleado emp = new Empleado();
                var imagenFile = collection.Files["imagen"];
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagenFile.CopyTo(memoryStream);
                        emp.Imagen = Convert.ToBase64String(memoryStream.ToArray()); // Almacena la imagen en Base64
                    }
                }

                // Asignar valores a la instancia emp
                emp.codEmpleado = collection["codEmpleado"].ToString();
                emp.IdTipoEmpleado = collection["idTipoEmpleado"].ToString();
                emp.Nombres = collection["nombres"].ToString();
                emp.Apellidos = collection["apellidos"].ToString();
                emp.Dui = collection["dui"].ToString();
                emp.Estado = !string.IsNullOrEmpty(collection["estado"]) && bool.Parse(collection["estado"]);
                emp.Usuario = collection["usuario"].ToString();
                emp.Clave = collection["clave"].ToString();

                // Verificar campos requeridos
                if (string.IsNullOrEmpty(emp.codEmpleado) ||
                    string.IsNullOrEmpty(emp.IdTipoEmpleado) ||
                    string.IsNullOrEmpty(emp.Nombres) ||
                    string.IsNullOrEmpty(emp.Apellidos) ||
                    string.IsNullOrEmpty(emp.Dui) ||
                    string.IsNullOrEmpty(emp.Imagen) || // Aquí ahora se asegura que 'emp.Imagen' no es null
                    string.IsNullOrEmpty(emp.Usuario) ||
                    string.IsNullOrEmpty(emp.Clave))
                {
                    TempData["ErrorMessage"] = "Todos los campos son requeridos.";
                    return View(); // Regresar a la vista con el mensaje de error
                }

                int resultado = mEmpleado.Ingresar(emp);

                if (resultado <= 0)
                {
                    TempData["ErrorMessage"] = "Hubo un error al guardar los datos.";
                    return View(); // Regresar a la vista con el mensaje de error
                }
                return RedirectToAction("Empleado");
            }
            else
            {
                // En caso de error, mostrar mensaje o volver a la vista de agregar
                ViewBag.ErrorMessage = "Error al guardar los datos.";
                return View();
            }
        }


        public IActionResult Eliminar(String codEmpleado)
        {
            //Definimos un objeto de tipo "MantenimientoTipoEmpleado"
            MantenimientoEmpleado MEmpleado = new MantenimientoEmpleado();

            //Llamamos al método "Borrar"
            MEmpleado.Borrar(codEmpleado);

            //Invocamos acción "Index"

            return RedirectToAction("Empleado");
        }

    }

}
