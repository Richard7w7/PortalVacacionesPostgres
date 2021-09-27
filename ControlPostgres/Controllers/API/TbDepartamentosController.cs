using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlPostgres.Contexto.Entities;

namespace ControlPostgres.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TbDepartamentosController : ControllerBase
    {
        private readonly BD_ControlVacacionesContext _context;

        public TbDepartamentosController(BD_ControlVacacionesContext context)
        {
            _context = context;
        }

        // GET: api/TbDepartamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TbDepartamento>>> GetTbDepartamentos()
        {
            return await _context.TbDepartamentos.ToListAsync();
        }

        // GET: api/TbDepartamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TbDepartamento>> GetTbDepartamento(int id)
        {
            var tbDepartamento = await _context.TbDepartamentos.FindAsync(id);

            if (tbDepartamento == null)
            {
                return NotFound();
            }

            return tbDepartamento;
        }

        // PUT: api/TbDepartamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTbDepartamento(int id, TbDepartamento tbDepartamento)
        {
            if (id != tbDepartamento.DeptoId)
            {
                return BadRequest();
            }

            _context.Entry(tbDepartamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TbDepartamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TbDepartamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TbDepartamento>> PostTbDepartamento(TbDepartamento tbDepartamento)
        {
            _context.TbDepartamentos.Add(tbDepartamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTbDepartamento", new { id = tbDepartamento.DeptoId }, tbDepartamento);
        }

        // DELETE: api/TbDepartamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTbDepartamento(int id)
        {
            var tbDepartamento = await _context.TbDepartamentos.FindAsync(id);
            if (tbDepartamento == null)
            {
                return NotFound();
            }

            _context.TbDepartamentos.Remove(tbDepartamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TbDepartamentoExists(int id)
        {
            return _context.TbDepartamentos.Any(e => e.DeptoId == id);
        }
    }
}
