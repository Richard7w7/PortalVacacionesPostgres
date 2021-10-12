﻿using ControlPostgres.Contexto.Entities;
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

    //seccion de enumerables para no tener numeros magicos
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
        enum EstadoSolicitud
        {
            Enviada = 1,
            Revision_I=2,
            Aprobada=4,
            Denegado=5
        }
        
        //aqui finaliza la seccion de enumerables

        /*aqui tenemos variables que utilizare en la clase*/
        BD_ControlVacacionesContext bd = new BD_ControlVacacionesContext();
        private static TbSolicitude usuario = new TbSolicitude();
        public static TbEmpleado login = new TbEmpleado();
        Repositorio puente = new Repositorio();
        Generadorpdf generador = new Generadorpdf();
        string? session = null;


        public IActionResult Perfil()
        {
            /*aca recibo lo que son los datos del empleado para mostrarlos en la vista de perfil*/
            // login = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
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

            return View();
        }else{
                return RedirectToAction("Index", "Home");
            }
                
        }
        public IActionResult PerfilColaborador()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult PerfilJefe()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

                return View(usuario);
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult PerfilDirector()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult PerfilEncargado()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult RetornarPerfil()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
                {
                    return RedirectToAction("PerfilColaborador", "Perfil");
                }
                else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
                {
                    return RedirectToAction("PerfilDirector", "Perfil");
                }
                else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
                {
                    return RedirectToAction("PerfilJefe", "Perfil");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult RegistrarSolicitud(TbSolicitude registro)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                registro.Empleado = usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                Boolean respuesta;
                try
                {
                    if (registro.DetallesSolicitud != null && registro.FechasSeleccionadas != null)
                    {


                        if (ModelState.IsValid)
                        {
                            respuesta = puente.CrearSolicitud(registro);
                            switch (respuesta)
                            {
                                case true:
                                    ModelState.Clear();
                                    return RedirectToAction("ListarSolicitudEmpleadoRevisiones");
                                case false:
                                    if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
                                    {
                                        return View("PerfilColaborador", usuario);
                                    }
                                    else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
                                    {
                                        return View("PerfilDirector", usuario);
                                    }
                                    else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
                                    {
                                        return View("PerfilJefe", usuario);
                                    }
                                    

                                    break;


                            }
                            return NotFound();
                        }
                        else
                        {
                            if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
                            {
                                return View("PerfilColaborador", usuario);
                            }
                            else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
                            {
                                return View("PerfilDirector", usuario);
                            }
                            else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
                            {
                                return View("PerfilJefe", usuario);
                            }
                            

                            return View(usuario);
                        }
                        
                    
                    }
                    else
                    {
                        if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
                        {
                            return View("PerfilColaborador", usuario);
                        }
                        else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
                        {
                            return View("PerfilDirector", usuario);
                        }
                        else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
                        {
                            return View("PerfilJefe", usuario);
                        }
                        

                        return View(usuario);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ListarSolicitudEmpleado()
        {

            session = HttpContext.Session.GetString("SessionUser");
            if (session != null) {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones)
                .Where(x => x.EmpleadoId == usuario.Empleado.EmpleadoId)
                .Where(x => x.EstadosId == (int)EstadoSolicitud.Aprobada || x.EstadosId == (int)EstadoSolicitud.Denegado).ToArray();

            return View(solicitudes.ToList());
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ListarSolicitudEmpleadoRevisiones()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones)
                    .Where(x => x.EmpleadoId == usuario.Empleado.EmpleadoId)
                    .Where(x => x.EstadosId == (int)EstadoSolicitud.Enviada || x.EstadosId == (int)EstadoSolicitud.Revision_I).ToArray();

                return View(solicitudes.ToList());
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        public IActionResult SolicitudesDepartamentoJefe()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));                                                         //.Where(x => x.Depto_ID == rama.Depto_ID)
                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();

                return View(solicitudes.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult SolicitudesDepartamentoDirector()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));                                                         //.Where(x => x.Depto_ID == rama.Depto_ID)
                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();

                return View(solicitudes.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult SolicitudesDepartamentoEncargado()
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();

                return View(solicitudes.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> Detalles(int? id)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
                if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
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
                    ViewBag.Estado = (int)tbSolicitude.EstadosId;
                    return View("DetallesColaborador", tbSolicitude);
                }
                else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
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
                    ViewBag.Estado = (int)tbSolicitude.EstadosId;
                    return View("DetallesDirector", tbSolicitude);
                }
                else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
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
                    ViewBag.Estado = (int)tbSolicitude.EstadosId;
                    return View("DetallesJefe", tbSolicitude);
                }

                return NotFound();
            }else
            { 
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DetallesDepa(int? id)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
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
                    int dato = tbSolicitude.EstadosId;
                    ViewData["Revision"] = dato;
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
                    ViewData["Revision"] = tbSolicitude.EstadosId;
                    var listadoestado = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 1)
                    .Where(x => x.EstadosId != 3).Where(x => x.EstadosId != 4), "EstadosId", "EstadosNombre");
                    ViewData["EstadoS"] = listadoestado;
                    return RedirectToAction("DetallesDepaEncargado", tbSolicitude);
                }
                return NotFound();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task <IActionResult> DetallesDepaEncargado(int? id)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
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
                ViewBag.Estado = tbSolicitude.EstadosId;
                ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 1)
                    .Where(x => x.EstadosId != 3).Where(x => x.EstadosId != 4), "EstadosId", "EstadosNombre");
                return View("DetallesDepaEncargado", tbSolicitude);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public async Task<IActionResult> DetallesDepaJefe(int? id)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
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

                ViewBag.Estado = Convert.ToInt32(tbSolicitude.EstadosId);
                ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 1).Where(x => x.EstadosId != 4), "EstadosId", "EstadosNombre");
                return View("DetallesDepaJefe", tbSolicitude);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public async Task<IActionResult> DetallesDepaDirector(int? id)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
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

                ViewBag.Estado = Convert.ToInt32(tbSolicitude.EstadosId);
                ViewData["EstadoS"] = new SelectList(bd.TbEstadosolicitudes.Where(x => x.EstadosId != 1)
                    .Where(x => x.EstadosId != 2).Where(x => x.EstadosId != 3), "EstadosId", "EstadosNombre");
                return View("DetallesDepaDirector", tbSolicitude);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ModificarDetallesDepaEncargado(int? id, TbSolicitude tbSolicitudes)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));

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
                    var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                    return View("SolicitudesDepartamentoEncargado", solicitudes.ToList());
                }
                return NotFound();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ModificarDetallesDepaJefe(int? id, TbSolicitude tbSolicitudes)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));


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

                if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Revision_I)
                {
                    tbSolicitude.EstadoSeleJefe = "Revision" + " "+usuario.Empleado.EmpleadoNombre1+" "+ usuario.Empleado.EmpleadoApellido1; 
                }
                else if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Denegado)
                {
                    tbSolicitude.EstadoSeleJefe = "Denegado" + " " + usuario.Empleado.EmpleadoNombre1 + " " + usuario.Empleado.EmpleadoApellido1;
                }


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
                var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                return View("SolicitudesDepartamentoJefe", solicitudes.ToList());
            }
            return NotFound();
        }else{
                return RedirectToAction("Index", "Home");
              }
        }
        [HttpPost]
        public async Task<IActionResult> ModificarDetallesDepaDirector(int? id, TbSolicitude tbSolicitudes)
        {
            session = HttpContext.Session.GetString("SessionUser");
            if (session != null)
            {
                usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));


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
                string comentario = tbSolicitude.Comentario;
                int estadoid = tbSolicitude.EstadosId;
                tbSolicitude.Comentario = tbSolicitudes.Comentario;
                tbSolicitude.EstadosId = tbSolicitudes.EstadosId;
                if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Aprobada)
                {
                    tbSolicitude.EstadoSeleDirector = "Aprovada" + " " + usuario.Empleado.EmpleadoNombre1 + " " + usuario.Empleado.EmpleadoApellido1;
                }
                else if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Denegado)
                {
                    tbSolicitude.EstadoSeleDirector = "Denegado" + " " + usuario.Empleado.EmpleadoNombre1 + " " + usuario.Empleado.EmpleadoApellido1;
                }
                if (id != tbSolicitude.SolicitudId)
                {
                    return NotFound();
                }

                if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Aprobada)
                {
                    int diasantiguos = (int)tbSolicitude.Empleado.EmpDiasvacaciones;
                    int diasresta = tbSolicitude.CantidadDias;
                    int diasrestantes = (int)(tbSolicitude.Empleado.EmpDiasvacaciones - diasresta);
                    tbSolicitude.Empleado.EmpDiasvacaciones = diasrestantes;
                    if (tbSolicitude.Empleado.EmpDiasvacaciones < 0)
                    {
                        tbSolicitude.Empleado.EmpDiasvacaciones = diasantiguos;
                        tbSolicitude.Comentario = "Lo sentimos la solicitud fue denegada automaticamente debido a que los dias seleccionados" +
                            " son mayor a la cantidad de dias restantes para vacaciones del empleado";
                        tbSolicitude.EstadosId = 5;
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
                            var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                            return View("SolicitudesDepartamentoDirector", solicitudes.ToList());
                        }
                        return NotFound();

                    }
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            bd.Update(tbSolicitude);
                            await bd.SaveChangesAsync();
                            string archivogenerado = generador.GenerateInvestorDocument(tbSolicitude);
                            if (string.IsNullOrWhiteSpace(archivogenerado))
                                return BadRequest("un error ha ocurrido al crear el archivo.");
                            var solicitudes3 = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                            return View("SolicitudesDepartamentoDirector", solicitudes3.ToList());
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

                    }
                    return NotFound();
                }

                else if (tbSolicitudes.EstadosId == (int)EstadoSolicitud.Denegado)
                {
                    int diasantiguos = (int)tbSolicitude.Empleado.EmpDiasvacaciones;
                    int diasresta = tbSolicitude.CantidadDias;
                    int diasrestantes = (int)(tbSolicitude.Empleado.EmpDiasvacaciones - diasresta);
                    tbSolicitude.Empleado.EmpDiasvacaciones = diasrestantes;
                    if (tbSolicitude.Empleado.EmpDiasvacaciones < 0)
                    {
                        tbSolicitude.Empleado.EmpDiasvacaciones = diasantiguos;
                        tbSolicitude.Comentario = "Lo sentimos la solicitud fue denegada automaticamente debido a que los dias seleccionados" +
                            " son mayor a la cantidad de dias restantes para vacaciones del empleado";
                        tbSolicitude.EstadosId = (int)EstadoSolicitud.Denegado;
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
                            var solicitudes = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                            return View("SolicitudesDepartamentoDirector", solicitudes.ToList());
                        }
                        return NotFound();

                    }
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            bd.Update(tbSolicitude);
                            await bd.SaveChangesAsync();
                            string archivogenerado = generador.GenerateInvestorDocument(tbSolicitude);
                            if (string.IsNullOrWhiteSpace(archivogenerado))
                                return BadRequest("un error ha ocurrido al crear el archivo.");
                            var solicitudes3 = bd.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones).Where(x => x.DeptoId == usuario.Empleado.DeptoId).ToArray();
                            return View("SolicitudesDepartamentoDirector", solicitudes3.ToList());
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

                    }
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
                    return View("DetallesDepaDirector", tbSolicitude);
                }
                return View("DetallesDepaDirector", tbSolicitude);
            }
            else { return RedirectToAction("Index", "Home");
            }
        }
        private bool TbSolicitudeExists(int id)
        {
            return bd.TbSolicitudes.Any(e => e.SolicitudId == id);
        }
        public async Task<IActionResult> ImprimirDocumento(int? id)
        {
            usuario.Empleado = JsonConvert.DeserializeObject<TbEmpleado>(HttpContext.Session.GetString("SessionUser"));
            var tbSolicitude = await bd.TbSolicitudes
              .Include(t => t.Cargo)
              .Include(t => t.Depto)
              .Include(t => t.Empleado)
              .Include(t => t.Estados)
              .Include(t => t.Vacaciones)
              .FirstOrDefaultAsync(m => m.SolicitudId == id);
            string archivogenerado = generador.GenerateInvestorDocumentUser(tbSolicitude);

            
            if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.MonitordeCamaras || usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.EncargadoTurno)
            {
                return RedirectToAction("PerfilColaborador", "Perfil");
            }
            else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.DirectorPM)
            {
                return RedirectToAction("PerfilDirector", "Perfil");
            }
            else if (usuario.Empleado.DeptoId == (int)Departamento.Monitoreo && usuario.Empleado.CargoId == (int)CargoDepaPM.JefeInmediatoMonitoreo)
            {
                return RedirectToAction("PerfilJefe", "Perfil");
            }
            
            HttpContext.Session.Remove("SessionUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
