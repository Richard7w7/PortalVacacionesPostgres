using ControlPostgres.Contexto.Entities;
using ControlPostgres.RepositorioClases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControlPostgres.Controllers
{
    enum Departamento
    {
        Monitoreo = 1
    }

    enum CargoDepaPM
    {

        DirectorPM = 2,
        JefeInmediatoMonitoreo = 3,
        MonitordeCamaras = 5,
        EncargadoTurno = 6

    }
    public class RegistroController : Controller
    {

        BD_ControlVacacionesContext bd = new BD_ControlVacacionesContext();
        Repositorio puente = new Repositorio();
        public static TbEmpleado empleado = new TbEmpleado();

        public ActionResult RegistrarEmp()
        {
            ViewData["Depto"] = new SelectList(bd.TbDepartamentos, "DeptoId", "DeptoNombre");
            ViewData["Cargos"] = new SelectList(bd.TbCargos, "CargoId", "CargoNombre");
            ViewData["Vacaciones"] = new SelectList(bd.TbVacaciones, "VacacionesId", "VacacionesEstado");

            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEmp(TbEmpleado model)
        {
            ViewData["Depto"] = new SelectList(bd.TbDepartamentos, "DeptoId", "DeptoNombre");
            ViewData["Cargos"] = new SelectList(bd.TbCargos, "CargoId", "CargoNombre");
            ViewData["Vacaciones"] = new SelectList(bd.TbVacaciones, "VacacionesId", "VacacionesEstado");

            Boolean logico;
            try
            {


                if (ModelState.IsValid)
                {
                    logico = puente.Registrar(model);

                    switch (logico)
                    {
                        case true:
                            ModelState.Clear();
                            return View("Login");
                        case false:
                            ViewBag.Registro = "No registro";
                            return View("RegistrarEmp");


                    }
                }
                else
                {
                    return View("RegistrarEmp");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Login()
        {
            ViewBag.Opcion = TempData["CambioContra"];
            return View();
        }
        [HttpPost]
        public IActionResult Login(TbEmpleado usuario)
        {
            ViewBag.Opcion = "";
            TempData["Logueo"] = "";
            TbEmpleado user = new TbEmpleado();

            try
            {


                if (!string.IsNullOrEmpty(usuario.EmpleadoCodigo) && !string.IsNullOrEmpty(usuario.EmpleadoContraseña))
                {
                    user = bd.TbEmpleados.FirstOrDefault(u => u.EmpleadoCodigo == usuario.EmpleadoCodigo && u.EmpleadoContraseña == usuario.EmpleadoContraseña);
                    if (user != null)
                    {
                        if (user.DeptoId == (int)Departamento.Monitoreo && user.CargoId == (int)CargoDepaPM.MonitordeCamaras || user.DeptoId == (int)Departamento.Monitoreo && user.CargoId == (int)CargoDepaPM.EncargadoTurno)
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                            TempData["Logueo"] = "Logueo Exitoso";
                            return RedirectToAction("PerfilColaborador", "Perfil");

                        }
                        else if (user.DeptoId == (int)Departamento.Monitoreo && user.CargoId == (int)CargoDepaPM.DirectorPM)
                        {
                            var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                            TempData["Logueo"] = "Logueo Exitoso";
                            return RedirectToAction("PerfilDirector", "Perfil");
                        }
                        else if (user.DeptoId == (int)Departamento.Monitoreo && user.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
                        {
                            var claims = new List<Claim>
                            {
                    new Claim(ClaimTypes.Name,user.EmpleadoCodigo)
                            };
                            var identity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);
                            var props = new AuthenticationProperties();
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                            HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(user));
                            TempData["Logueo"] = "Logueo Exitoso";
                            return RedirectToAction("PerfilJefe", "Perfil");
                        }

                    }


                }
                else
                {
                    ViewBag.Opcion = "Campos Vacios";
                    return View("Login");
                }
                ViewBag.Opcion = "Usuario Incorrecto";
                return View("Login");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult RecuperarContra()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RecuperarContra(TbContraseña contra)
        {
            if (!string.IsNullOrEmpty(contra.codigoempleado) && !string.IsNullOrEmpty(contra.fechanacimiento.ToString()) && !string.IsNullOrEmpty(contra.telefono))
            {
                TbEmpleado user = new TbEmpleado();

                user = bd.TbEmpleados.FirstOrDefault(u => u.EmpleadoCodigo == contra.codigoempleado && u.FechaNacimiento == contra.fechanacimiento && u.EmpleadoTelefono == contra.telefono);
                if (user != null)
                {

                    HttpContext.Session.SetString("SessionContra", JsonConvert.SerializeObject(user));
                    TempData["DatosCorrectos"] = "datos correctos";
                    return RedirectToAction("ConfirmaContraseña");
                }
                else
                {
                    ViewBag.Opcion = "Credenciales no coinciden";
                    return View();
                }
            }
            else
            {
                ViewBag.Opcion = "Campos vacios";
                return View();
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("SessionUser");
            TempData["Salir"] = "Salio";
            return RedirectToAction("Index", "Home");
        }
        private bool TbEmpleadoExists(int id)
        {
            return bd.TbEmpleados.Any(e => e.EmpleadoId == id);
        }

        public IActionResult ConfirmaContraseña()
        {
            ViewBag.Opcion = TempData["DatosCorrectos"];
            
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmaContraseña(TbReContra recontra)
        {
            TbEmpleado empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionContra"));
            if(!string.IsNullOrEmpty(recontra.Contra) && !string.IsNullOrEmpty(recontra.RepetirContra)) { 
            if (recontra.Contra == recontra.RepetirContra)
            {
                if (ModelState.IsValid)
                {

                    var usuario = bd.TbEmpleados.Where(u => u.EmpleadoId == empleado.EmpleadoId).FirstOrDefault();
                    usuario.EmpleadoContraseña = recontra.RepetirContra;
                    bd.SaveChanges();
                    HttpContext.Session.Remove("SessionContra");
                        TempData["CambioContra"] = "cambio correcto";
                    return RedirectToAction("Login");

                }
                else
                {
                    ViewBag.Problemacontra = "Lo sentimos ha ocurrido un problema, por favor hable con el administrador del portal";
                    return View();
                }
            }
            else 
            {
                   
                    ViewBag.Opcion = "no iguales";
                    return View();
                }
            }
            else
            {
                ViewBag.Opcion = "Campos vacios";
                return View();
            }
        }
    }

}
