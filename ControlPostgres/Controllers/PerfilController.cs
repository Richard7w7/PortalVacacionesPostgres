using ControlPostgres.Contexto.Entities;
using ControlPostgres.RepositorioClases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPostgres.Controllers
{
    public class PerfilController : Controller
    {
        BD_ControlVacacionesContext bd = new BD_ControlVacacionesContext();
        private static TbSolicitude usuario = new TbSolicitude();
        Repositorio puente = new Repositorio();
        
        public IActionResult Perfil()
        {
            /*aca recibo lo que son los datos del empleado para mostrarlos en la vista de perfil*/
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
            if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 5 || usuario.Empleado.CargoId == 4)
            {
                return View("PerfilColaborador",usuario);
            }else if(usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 2)
            {
                return View("PerfilJefe",usuario);
            }else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 3)
            {
                return View("PerfilEncargado",usuario);
            }

            return View();
                
        }
        public IActionResult PerfilColaborador()
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

            return View(usuario);
        }
        public IActionResult PerfilJefe()
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

            return View(usuario);
        }
        public IActionResult PerfilEncargado()
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
            return View(usuario);
        }

        [HttpPost]
        public IActionResult RegistrarSolicitud(TbSolicitude registro)
        {
            registro.Empleado = usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
            Boolean respuesta;
            try
            {
                if (ModelState.IsValid)
                {
                    respuesta = puente.CrearSolicitud(registro);
                    switch (respuesta)
                    {
                        case true:
                            ModelState.Clear();
                            return RedirectToAction("ListarSolicitudEmpleado");
                        case false:
                            if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 5 || usuario.Empleado.CargoId == 4)
                            {
                                return View("PerfilColaborador", usuario);
                            }
                            else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 2)
                            {
                                return View("PerfilJefe", usuario);
                            }
                            else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 3)
                            {
                                return View("PerfilEncargado", usuario);
                            }

                            break;

                           
                    }
                    return NotFound();
                }
                else
                {
                    return View("Perfil");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult ListarSolicitudEmpleado()
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
            var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.EmpleadoId == usuario.Empleado.EmpleadoId).ToArray();

            return View(solicitudes.ToList());
        }

        public IActionResult SolicitudesDepartamento()
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));                                                         //.Where(x => x.Depto_ID == rama.Depto_ID)
            var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();

            return View(solicitudes.ToList());
        }

        public async Task<IActionResult> Detalles(int? id)
        {
            if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 5)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbSolicitude = await bd.TbSolicitudes
                    .Include(t => t.Cargo)
                    .Include(t => t.Depto)
                    .Include(t => t.Empleado)
                    .Include(t => t.Estados)
                    .Include(t => t.Vacaciones)
                    .FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (tbSolicitude == null)
                {
                    return NotFound();
                }

                return View("DetallesColaborador",tbSolicitude);
            }
            else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 2)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbSolicitude = await bd.TbSolicitudes
                    .Include(t => t.Cargo)
                    .Include(t => t.Depto)
                    .Include(t => t.Empleado)
                    .Include(t => t.Estados)
                    .Include(t => t.Vacaciones)
                    .FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (tbSolicitude == null)
                {
                    return NotFound();
                }

                return View("DetallesJefe",tbSolicitude);
            }
            else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 4)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbSolicitude = await bd.TbSolicitudes
                    .Include(t => t.Cargo)
                    .Include(t => t.Depto)
                    .Include(t => t.Empleado)
                    .Include(t => t.Estados)
                    .Include(t => t.Vacaciones)
                    .FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (tbSolicitude == null)
                {
                    return NotFound();
                }

                return View("DetallesEncargado", tbSolicitude);
            }

            return NotFound();
        }

        public async Task<IActionResult> DetallesDepa(int? id)
        {
            if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 2)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbSolicitude = await bd.TbSolicitudes
                    .Include(t => t.Cargo)
                    .Include(t => t.Depto)
                    .Include(t => t.Empleado)
                    .Include(t => t.Estados)
                    .Include(t => t.Vacaciones)
                    .FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (tbSolicitude == null)
                {
                    return NotFound();
                }
                var listadoestado = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 2)
                .Where(x => x.EstadosId != 1), "EstadosId", "EstadosNombre");
                ViewData["EstadoS"] = listadoestado;
                return View("DetallesDepaJefe", tbSolicitude);
            }
            else if (usuario.Empleado.DeptoId == 1 && usuario.Empleado.CargoId == 4)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tbSolicitude = await bd.TbSolicitudes
                    .Include(t => t.Cargo)
                    .Include(t => t.Depto)
                    .Include(t => t.Empleado)
                    .Include(t => t.Estados)
                    .Include(t => t.Vacaciones)
                    .FirstOrDefaultAsync(m => m.SolicitudId == id);
                if (tbSolicitude == null)
                {
                    return NotFound();
                }
                //ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
                var listadoestado = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 1)
                .Where(x => x.EstadosId != 3).Where(x => x.EstadosId != 4), "EstadosId", "EstadosNombre");
                ViewData["EstadoS"] = listadoestado;
                return View("DetallesDepaEncargado", tbSolicitude);
            }
            return NotFound();
        }

        public async Task <IActionResult> DetallesDepaEncargado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSolicitude = await bd.TbSolicitudes
                .Include(t => t.Cargo)
                .Include(t => t.Depto)
                .Include(t => t.Empleado)
                .Include(t => t.Estados)
                .Include(t => t.Vacaciones)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (tbSolicitude == null)
            {
                return NotFound();
            }
            ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
            return View("DetallesDepaEncargado", tbSolicitude);

        }
        public async Task<IActionResult> DetallesDepaJefe(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSolicitude = await bd.TbSolicitudes
                .Include(t => t.Cargo)
                .Include(t => t.Depto)
                .Include(t => t.Empleado)
                .Include(t => t.Estados)
                .Include(t => t.Vacaciones)
                .FirstOrDefaultAsync(m => m.SolicitudId == id);
            if (tbSolicitude == null)
            {
                return NotFound();
            }
            ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
            return View("DetallesDepaJefe", tbSolicitude);
        }
        public async Task<IActionResult> ModificarDetallesDepaEncargado(int? id, TbSolicitude tbSolicitudes)
        {

            if (id == null)
            {
                return NotFound();
            }
            ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
            var tbSolicitude = await bd.TbSolicitudes
               .Include(t => t.Cargo)
               .Include(t => t.Depto)
               .Include(t => t.Empleado)
               .Include(t => t.Estados)
               .Include(t => t.Vacaciones)
               .FirstOrDefaultAsync(m => m.SolicitudId == id);

            tbSolicitude.Comentario = tbSolicitudes.Comentario;
            tbSolicitude.EstadosId = tbSolicitudes.EstadosId;
            if (id != tbSolicitude.SolicitudId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(tbSolicitude);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbSolicitudeExists(tbSolicitude.SolicitudId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View("DetallesDepaEncargado", tbSolicitude);
            }
            return View("DetallesDepaEncargado", tbSolicitude);
        }
        public async Task<IActionResult> ModificarDetallesDepaJefe(int? id,TbSolicitude tbSolicitudes)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
            var tbSolicitude = await bd.TbSolicitudes
               .Include(t => t.Cargo)
               .Include(t => t.Depto)
               .Include(t => t.Empleado)
               .Include(t => t.Estados)
               .Include(t => t.Vacaciones)
               .FirstOrDefaultAsync(m => m.SolicitudId == id);

            tbSolicitude.Comentario = tbSolicitudes.Comentario;
            tbSolicitude.EstadosId = tbSolicitudes.EstadosId;
            if (id != tbSolicitude.SolicitudId)
            {
                return NotFound();
            }

            if(tbSolicitudes.EstadosId == 4)
            {
                int diasresta = tbSolicitude.CantidadDias;
                
                if (ModelState.IsValid)
                {
                    try
                    {
                        bd.Update(tbSolicitude);
                        await bd.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TbSolicitudeExists(tbSolicitude.SolicitudId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    //return RedirectToAction(nameof(Index));
                    return View("DetallesDepaJefe", tbSolicitude);
                }

            }

            if (ModelState.IsValid)
            {
                try
                {
                    bd.Update(tbSolicitude);
                    await bd.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbSolicitudeExists(tbSolicitude.SolicitudId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return View("DetallesDepaJefe", tbSolicitude);
            }
            return View("DetallesDepaJefe", tbSolicitude);
        }
        private bool TbSolicitudeExists(int id)
        {
            return bd.TbSolicitudes.Any(e => e.SolicitudId == id);
        }
    }
}
