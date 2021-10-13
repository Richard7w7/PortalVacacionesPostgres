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
    public class TbEmpleadoesController : Controller
    {
        private readonly BD_ControlVacacionesContext _context;

        public TbEmpleadoesController(BD_ControlVacacionesContext context)
        {
            _context = context;
        }

        // GET: TbEmpleadoes
        public async Task<IActionResult> Index()
        {
            var bD_ControlVacacionesContext = _context.TbEmpleados.Include(t => t.Cargo).Include(t => t.Depto).Include(t => t.Vacaciones);
            return View(await bD_ControlVacacionesContext.ToListAsync());
        }

        // GET: TbEmpleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado = await _context.TbEmpleados
                .Include(t => t.Cargo)
                .Include(t => t.Depto)
                .Include(t => t.Vacaciones)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }

            return View(tbEmpleado);
        }

        // GET: TbEmpleadoes/Create
        public IActionResult Create()
        {
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion");
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion");
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado");
            return View();
        }

        // POST: TbEmpleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,CargoId,DeptoId,VacacionesId,EmpleadoCodigo,EmpleadoContraseña,EmpleadoNombre1,EmpleadoNombre2,EmpleadoApellido1,EmpleadoApellido2,ApellidoCasada,FechaNacimiento,EmpleadoTelefono,EmpleadoDireccion,EmpleadoEstado,FechaIngreso,EmpleadoPermiso,EmpladoUltimavacafin,EmpleadoUltimavacainicio,EmpDiasvacaciones")] TbEmpleado tbEmpleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEmpleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbEmpleado.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbEmpleado.DeptoId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbEmpleado.VacacionesId);
            return View(tbEmpleado);
        }

        // GET: TbEmpleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado = await _context.TbEmpleados.FindAsync(id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbEmpleado.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbEmpleado.DeptoId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbEmpleado.VacacionesId);
            return View(tbEmpleado);
        }

        // POST: TbEmpleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,CargoId,DeptoId,VacacionesId,EmpleadoCodigo,EmpleadoContraseña,EmpleadoNombre1,EmpleadoNombre2,EmpleadoApellido1,EmpleadoApellido2,ApellidoCasada,FechaNacimiento,EmpleadoTelefono,EmpleadoDireccion,EmpleadoEstado,FechaIngreso,EmpleadoPermiso,EmpladoUltimavacafin,EmpleadoUltimavacainicio,EmpDiasvacaciones")] TbEmpleado tbEmpleado)
        {
            if (id != tbEmpleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEmpleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEmpleadoExists(tbEmpleado.EmpleadoId))
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
            ViewData["CargoId"] = new SelectList(_context.TbCargos, "CargoId", "CargoDescripcion", tbEmpleado.CargoId);
            ViewData["DeptoId"] = new SelectList(_context.TbDepartamentos, "DeptoId", "DeptoDescripcion", tbEmpleado.DeptoId);
            ViewData["VacacionesId"] = new SelectList(_context.TbVacaciones, "VacacionesId", "VacacionesEstado", tbEmpleado.VacacionesId);
            return View(tbEmpleado);
        }

        // GET: TbEmpleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEmpleado = await _context.TbEmpleados
                .Include(t => t.Cargo)
                .Include(t => t.Depto)
                .Include(t => t.Vacaciones)
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (tbEmpleado == null)
            {
                return NotFound();
            }

            return View(tbEmpleado);
        }

        // POST: TbEmpleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbEmpleado = await _context.TbEmpleados.FindAsync(id);
            _context.TbEmpleados.Remove(tbEmpleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEmpleadoExists(int id)
        {
            return _context.TbEmpleados.Any(e => e.EmpleadoId == id);
        }
    }
}
