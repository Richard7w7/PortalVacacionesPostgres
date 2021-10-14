using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlPostgres.Contexto.Entities;

namespace ControlPostgres.Controllers
{
    public class TbSolicitudesController : Controller
    {
        private readonly BD_ControlVacacionesContext _context;

        public TbSolicitudesController(BD_ControlVacacionesContext context)
        {
            _context = context;
        }

        public IActionResult ListarSolicitudEmpleado()
        {
            return View();
        }
        // GET: TbSolicitudes
        public async Task<IActionResult> Index()
        {
            var bD_ControlVacacionesContext = _context.TbSolicitudes.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Empleado).Include(t => t.Estados).Include(t => t.Vacaciones);
            return View(await bD_ControlVacacionesContext.ToListAsync());
        }

        // GET: TbSolicitudes/Details/5
        public async Task<IActionResult> Details(int? id)   
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSolicitude = await _context.TbSolicitudes
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

            return View(tbSolicitude);
        }

        // GET: TbSolicitudes/Create
        public IActionResult Create()
        {
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion");
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion");
            ViewData["EmpleadoId"] = new SelectList(_context.TbEmpleados, "EmpleadoId", "EmpleadoApellido1");
            ViewData["EstadosId"] = new SelectList(_context.TbEstadosolicitudes, "EstadosId", "EstadosNombre");
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado");
            return View();
        }

        // POST: TbSolicitudes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolicitudId,EmpleadoId,CargoId,DeptoId,VacacionesId,EstadosId,SolicitudFecha,DetallesSolicitud,FechasSeleccionadas,CantidadDias,Comentario")] TbSolicitude tbSolicitude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbSolicitude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbSolicitude.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbSolicitude.DeptoId);
            ViewData["EmpleadoId"] = new SelectList(_context.TbEmpleados, "EmpleadoId", "EmpleadoApellido1", tbSolicitude.EmpleadoId);
            ViewData["EstadosId"] = new SelectList(_context.TbEstadosolicitudes, "EstadosId", "EstadosNombre", tbSolicitude.EstadosId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbSolicitude.VacacionesId);
            return View(tbSolicitude);
        }
        
        // GET: TbSolicitudes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSolicitude = await _context.TbSolicitudes.FindAsync(id);
            if (tbSolicitude == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbSolicitude.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbSolicitude.DeptoId);
            ViewData["EmpleadoId"] = new SelectList(_context.TbEmpleados, "EmpleadoId", "EmpleadoApellido1", tbSolicitude.EmpleadoId);
            ViewData["EstadosId"] = new SelectList(_context.TbEstadosolicitudes, "EstadosId", "EstadosNombre", tbSolicitude.EstadosId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbSolicitude.VacacionesId);
            return View(tbSolicitude);
        }

        // POST: TbSolicitudes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolicitudId,EmpleadoId,CargoId,DeptoId,VacacionesId,EstadosId,SolicitudFecha,DetallesSolicitud,FechasSeleccionadas,CantidadDias,Comentario")] TbSolicitude tbSolicitude)
        {
            if (id != tbSolicitude.SolicitudId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbSolicitude);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbSolicitude.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbSolicitude.DeptoId);
            ViewData["EmpleadoId"] = new SelectList(_context.TbEmpleados, "EmpleadoId", "EmpleadoApellido1", tbSolicitude.EmpleadoId);
            ViewData["EstadosId"] = new SelectList(_context.TbEstadosolicitudes, "EstadosId", "EstadosNombre", tbSolicitude.EstadosId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbSolicitude.VacacionesId);
            return View(tbSolicitude);
        }

        // GET: TbSolicitudes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbSolicitude = await _context.TbSolicitudes
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

            return View(tbSolicitude);
        }

        // POST: TbSolicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbSolicitude = await _context.TbSolicitudes.FindAsync(id);
            _context.TbSolicitudes.Remove(tbSolicitude);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbSolicitudeExists(int id)
        {
            return _context.TbSolicitudes.Any(e => e.SolicitudId == id);
        }
    }
}
