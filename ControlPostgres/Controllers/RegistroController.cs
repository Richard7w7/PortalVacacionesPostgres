using ControlPostgres.Contexto.Entities;
using ControlPostgres.RepositorioClases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ControlPostgres.Controllers
{
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
            ViewData["Rol"] = new SelectList(bd.TbRoles, "RolId", "RolNombre");
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarEmp(TbEmpleado model)
        {
            ViewData["Depto"] = new SelectList(bd.TbDepartamentos, "DeptoId", "DeptoNombre");
            ViewData["Cargos"] = new SelectList(bd.TbCargos, "CargoId", "CargoNombre");
            ViewData["Vacaciones"] = new SelectList(bd.TbVacaciones, "VacacionesId", "VacacionesEstado");
            ViewData["Rol"] = new SelectList(bd.TbRoles, "RolId", "RolNombre");
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(TbEmpleado usuario)
        {
            TbEmpleado user = new TbEmpleado();

            try
            {
                if (!string.IsNullOrEmpty(usuario.EmpleadoCodigo) && !string.IsNullOrEmpty(usuario.EmpleadoContraseña))
                {
                    user = bd.TbEmpleados.FirstOrDefault(u => u.EmpleadoCodigo == usuario.EmpleadoCodigo && u.EmpleadoContraseña == usuario.EmpleadoContraseña);
                    if (user != null)
                    {
                        if (user.DeptoId == 1 && user.CargoId == 5)
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
                            return RedirectToAction("PerfilColaborador", "Perfil");

                        }
                        else if (user.DeptoId == 1 && user.CargoId == 2)
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
                            return RedirectToAction("PerfilJefe", "Perfil");
                        }
                        else if (user.DeptoId == 1 && user.CargoId == 4)
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
                            return RedirectToAction("PerfilEncargado", "Perfil");
                        }

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
